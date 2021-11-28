using System;
using OpenTK;
using OpenTK.Input;
using RGEngine.BaseClasses;
using RGEngine.Input;
using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine;
using System.Linq;
using Racing.Prizes;
using RGEngine.Multiplayer;

namespace Racing.Objects
{
    /// <summary>
    /// Defines an abstract car object.
    /// </summary>
    public abstract class Car : GameObject, ICollidable
    {
        private const float MinCarVelocity = 6f;
        private const float WheelBaseLength = 80f;
        private const float BaseCarMass = 1200f;
        private const float BaseCarFriction = 750f;
        private const float FuelConsumptionTime = 5f;

        private readonly Vector2 CarStartPosition = new Vector2(150f, 150f);
        private readonly Vector2 CarPolyColliderSize = new Vector2(110f, 45f);

        private float _fuelLevel;
        /// <summary>
        /// Responsible for car front wheels steering angle.
        /// </summary>
        private float _steeringAngle;
        /// <summary>
        /// Defines current car direction on scene (in radians).
        /// </summary>
        private float _currentCarDirectionAngle;
        /// <summary>
        /// Keeps a driving mode. Drive or Reverse.
        /// </summary>
        private DrivingModes _currentDrivingMode;

        private Vector2 _frontWheelPosition;
        private Vector2 _backWheelPosition;

        private bool[] _laps;
        private int _beingLocatedOnFinishLine;
        private int _lapsPassed;

        private readonly RigidBody2D _rigidBody2D;
        protected SpriteRenderer spriteRenderer;

        /// <summary>
        /// Ctor that set a basic car object settings properly.
        /// </summary>
        public Car()
        {
            spriteRenderer = (SpriteRenderer)AddComponent("SpriteRenderer");
            _rigidBody2D = (RigidBody2D)AddComponent("RigidBody2D");
            _rigidBody2D.mass = BaseCarMass;
            _rigidBody2D.frictionConst = BaseCarFriction;

            // Set defaults for a default car.
            Properties = new CarProps(this);
            _fuelLevel = Properties.MaxFuelLevel;

            SetStartCarPosition(CarStartPosition);

            collider = new PolyCollider(this, CarPolyColliderSize);
            collider.ColliderTriggered += FinishLine_ColliderTriggered;
            collider.ColliderTriggered += Prize_ColliderTriggered;

            // Difine finished laps array.
            _laps = new bool[5 + 1];
        }

        public Car(bool isPlayable)
            : this()
        {
            IsControlledByLocalUser = isPlayable;
        }

        /// <summary>
        /// Keeps all car properties.
        /// </summary>
        public CarProps Properties { get; set; }

        public bool IsControlledByLocalUser { get; private set; }

        public string Id { get; set; }

        /// <summary>
        /// Responsible for getting and setting current fuel level.
        /// </summary>
        public float FuelLevel
        {
            get => _fuelLevel;
            set
            {
                if (_fuelLevel + value >= Properties.MaxFuelLevel)
                {
                    _fuelLevel = Properties.MaxFuelLevel;
                }
                else
                {
                    _fuelLevel = value;
                }
            }
        }

        /// <summary>
        /// Responsible for counting laps that were passed.
        /// </summary>
        public int LapsPassed
        {
            get
            {
                if (IsControlledByLocalUser)
                {
                    var result = 0;
                    for (int i = 0; i < _laps.Length; i++)
                    {
                        if (_laps[i])
                        {
                            result++;
                        }
                    }
                    return result;
                }
                else
                {
                    return _lapsPassed;
                }
            }
        }

        /// <summary>
        /// Event that raised when End of race occured.
        /// </summary>
        public event EventHandler<GameEventArgs> EndedRace;
        /// <summary>
        /// Invokes EndedRace event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnEndedRace(GameEventArgs e)
        {
            EndedRace?.Invoke(this, e);
        }


        /// <summary>
        /// Override of FixedUpdate method.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            if (UdpHandlerObject.ReceivedMessage.IsGameEnded)
            {
                OnEndedRace(new GameEventArgs(UdpHandlerObject.ReceivedMessage.Id));
            }

            if (UdpHandlerObject.ReceivedMessage.PrizeId != 0)
            {
                for (int i = 0; i < EngineCore.GameObjects.Count; i++)
                {
                    if (EngineCore.GameObjects[i] is Prize prize && prize.Id == UdpHandlerObject.ReceivedMessage.PrizeId)
                    {
                        PolyCollider.allCollidersAttached.Remove(prize);
                        EngineCore.RemoveGameObject(prize);
                    }
                }
            }

            if (IsControlledByLocalUser)
            {
                var steer = MathHelper.DegreesToRadians(_steeringAngle);

                var deltaBackWheel = Vector2.Zero;
                var deltaFrontWheel = Vector2.Zero;

                deltaBackWheel.X = _rigidBody2D.velocity * (float)fixedDeltaTime *
                    (float)Math.Cos(_currentCarDirectionAngle);
                deltaBackWheel.Y = _rigidBody2D.velocity * (float)fixedDeltaTime *
                    (float)Math.Sin(_currentCarDirectionAngle);

                // Calculations that push the front wheel forward and conserve the wheelBase distance.
                var distance = _rigidBody2D.velocity * (float)fixedDeltaTime;
                var B = (WheelBaseLength - distance) * Math.Cos(steer);
                var C = distance * (2 * WheelBaseLength - distance);
                var calc = Math.Sqrt(B * B + C) - B;

                deltaFrontWheel.X = (float)(calc * Math.Cos(_currentCarDirectionAngle + steer));
                deltaFrontWheel.Y = (float)(calc * Math.Sin(_currentCarDirectionAngle + steer));

                _backWheelPosition += deltaBackWheel;
                _frontWheelPosition += deltaFrontWheel;
                Position = (_frontWheelPosition + _backWheelPosition) / 2;

                // Detect and resolve collisions.
                foreach (var @object in EngineCore.GameObjects.ToList())
                {
                    // Car DOES NOT collide with other car.
                    if (@object is ICollidable && !(@object is Car))
                    {
                        if (!ReferenceEquals(this, @object))
                        {
                            if (collider.DetectCollision(@object))
                            {
                                if (!(@object is INonResolveable))
                                {
                                    _backWheelPosition -= 1.5f * deltaBackWheel;
                                    _frontWheelPosition -= 1.5f * deltaFrontWheel;
                                    _rigidBody2D.velocity /= -2.75f;
                                }
                            }
                        }
                    }
                }

                Position = (_frontWheelPosition + _backWheelPosition) / 2;
                _currentCarDirectionAngle = (float)Math.Atan2(
                    _frontWheelPosition.Y - _backWheelPosition.Y,
                    _frontWheelPosition.X - _backWheelPosition.X);
                Rotation = MathHelper.RadiansToDegrees(_currentCarDirectionAngle);

                ApplyFuelConsumprion(fixedDeltaTime);

                if (EngineCore.IsMultiplayerEnabled)
                {
                    UdpHandlerObject.MessageToSend.Id = Id;
                    UdpHandlerObject.MessageToSend.CarPosition = Position;
                    UdpHandlerObject.MessageToSend.CarRotation = Rotation;
                    UdpHandlerObject.MessageToSend.Fuel = FuelLevel;
                    UdpHandlerObject.MessageToSend.Laps = LapsPassed;
                }
            }
            else
            {
                if (Id == UdpHandlerObject.ReceivedMessage.Id)
                {
                    Position = UdpHandlerObject.ReceivedMessage.CarPosition;
                    Rotation = UdpHandlerObject.ReceivedMessage.CarRotation;
                    _fuelLevel = UdpHandlerObject.ReceivedMessage.Fuel;
                    _lapsPassed = UdpHandlerObject.ReceivedMessage.Laps;
                }
            }
        }

        #region ColliderTriggered handlers

        /// <summary>
        /// Handles the ColliderTriggered event when car collides a finish line.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishLine_ColliderTriggered(object sender, CollisionEventArgs e)
        {
            if (ReferenceEquals(this, e.one))
            {
                // Checks for finishline crossing.
                if (e.another is FinishLine)
                {
                    if ((Rotation < 90f && Rotation > -90f && _rigidBody2D.velocity > 0) ||
                        (Rotation > 90f && Rotation < -90f && _rigidBody2D.velocity < 0))
                    {
                        if (!_laps[_lapsPassed])
                        {
                            _laps[_lapsPassed] = true;
                        }

                        _beingLocatedOnFinishLine++;
                    }

                    if (LapsPassed == 5 + 1)
                    {
                        OnEndedRace(new GameEventArgs(Id));
                    }
                }
                else
                {
                    if (e.another is OuterFinishLine)
                    {
                        if (_beingLocatedOnFinishLine > 0)
                        {
                            _lapsPassed = LapsPassed;
                            _beingLocatedOnFinishLine = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the ColliderTriggered event when car collides a prize.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Prize_ColliderTriggered(object sender, CollisionEventArgs e)
        {
            foreach (var gameObject in EngineCore.GameObjects.ToList())
            {
                if (gameObject is Prize prize)
                {
                    if (ReferenceEquals(gameObject, e.another))
                    {
                        object obj = prize.Id;
                        UdpHandlerObject.MessageToSend.PrizeId = (int)obj;
                        prize.PickUp(this);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Applies a fuel consuption throught time.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        /// <returns></returns>
        public void ApplyFuelConsumprion(double fixedDeltaTime)
        {
            // End of race condition.
            if (_fuelLevel <= 0.001)
            {
                // Choose another car as the winner.
                if (Id == "Black")
                {
                    Id = "Purple";
                }
                else if (Id == "Purple")
                {
                    Id = "Black";
                }
                    
                OnEndedRace(new GameEventArgs(Id));
                return;
            }

            if (_rigidBody2D.velocity == 0)
            {
                _fuelLevel -= Properties.IdleFuelConsumption * (float)fixedDeltaTime / FuelConsumptionTime;
            }
            else
            {
                _fuelLevel -= Properties.DrivingFuelConsumption * (float)fixedDeltaTime / FuelConsumptionTime;
            }
        }

        #region User Input Methods

        /// <summary>
        /// Gets user input for the main car control.
        /// </summary>
        /// <param name="gas"></param>
        /// <param name="brake"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        protected virtual void GetUserInput(Key gas, Key brake, Key left, Key right)
        {
            // Check for max and min velocity-values.
            if (_rigidBody2D.velocity >= Properties.MaxVelocity)
            {
                _rigidBody2D.velocity = Properties.MaxVelocity;
            }
            if (_rigidBody2D.velocity <= Properties.MaxVelocityReverse)
            {
                _rigidBody2D.velocity = Properties.MaxVelocityReverse;
            }

            // Fixing a STOP-point.
            if (_rigidBody2D.velocity <= MinCarVelocity && _rigidBody2D.velocity >= -MinCarVelocity)
            {
                _rigidBody2D.velocity = 0;
            }

            if (InputController.CurrentKeyboardState.IsKeyDown(gas))
            {
                _rigidBody2D.engineForce = (int)_currentDrivingMode * Properties.MaxEngineForce;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(brake))
            {
                if (_rigidBody2D.velocity == 0)
                {
                    _rigidBody2D.breakingForce = 0;
                }
                else if (_rigidBody2D.velocity > 0)
                {
                    _rigidBody2D.breakingForce = Properties.MaxBreakingForce;
                }
                else
                {
                    _rigidBody2D.breakingForce = -Properties.MaxBreakingForce;
                }
            }
            else
            {
                _rigidBody2D.breakingForce = 0;
                _rigidBody2D.engineForce = 0;
            }

            if (InputController.CurrentKeyboardState.IsKeyDown(left))
            {
                _steeringAngle = -Properties.MaxSteeringAngle;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(right))
            {
                _steeringAngle = Properties.MaxSteeringAngle;
            }
            else
            {
                _steeringAngle = 0f;
            }
        }

        /// <summary>
        /// Gets user input to update a gearbox state.
        /// </summary>
        /// <param name="reverse"></param>
        /// <param name="drive"></param>
        protected virtual void UpdateGearboxState(Key reverse, Key drive)
        {
            if (InputController.CurrentKeyboardState.IsKeyDown(drive))
            {
                _currentDrivingMode = DrivingModes.Drive;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(reverse))
            {
                _currentDrivingMode = DrivingModes.Reverse;
            }
        }

        #endregion

        /// <summary>
        /// Sets a start car position.
        /// </summary>
        /// <param name="startPosition"></param>
        protected virtual void SetStartCarPosition(Vector2 startPosition)
        {
            Position = startPosition;
            _frontWheelPosition = Position + WheelBaseLength / 2 *
                new Vector2((float)Math.Cos(_currentCarDirectionAngle), (float)Math.Sin(_currentCarDirectionAngle));
            _backWheelPosition = Position - WheelBaseLength / 2 *
                new Vector2((float)Math.Cos(_currentCarDirectionAngle), (float)Math.Sin(_currentCarDirectionAngle));
        }

        /// <summary>
        /// Defines main driving modes.
        /// </summary>
        private enum DrivingModes
        {
            Reverse = -1,
            Neutral = 0,
            Drive = 1
        }
    }
}
