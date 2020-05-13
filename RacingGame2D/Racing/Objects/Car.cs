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

namespace Racing.Objects
{
    /// <summary>
    /// Defines an abstract car object.
    /// </summary>
    public abstract class Car : GameObject, ICollidable
    {
        /// <summary>
        /// Ctor that set a basic car object settings properly.
        /// </summary>
        public Car()
        {
            spriteRenderer = (SpriteRenderer)AddComponent("SpriteRenderer");
            rigidBody2D = (RigidBody2D)AddComponent("RigidBody2D");

            // Set defaults for a default car.
            properties = new CarProps(this);
            this.fuelLevel = properties.MaxFuelLevel;

            // Set default wheelBase value and start car position.
            wheelBase = 80f;
            SetStartCarPosition(new Vector2(150f, 150f));

            // Set default RigidBody parameters for a basic Car object.
            rigidBody2D.mass = 1200f;
            rigidBody2D.frictionConst = 750f;

            base.collider = new PolyCollider(this, new Vector2(110f, 45f));
            base.collider.ColliderTriggered += FinishLine_ColliderTriggered;
            base.collider.ColliderTriggered += Prize_ColliderTriggered;

            // Difine finished laps array.
            this.laps = new bool[5 + 1];
        }

        #region Fields and Props
        
        /// <summary>
        /// SpriteRenderer component.
        /// </summary>
        protected SpriteRenderer spriteRenderer;
        /// <summary>
        /// RigidBody2D component.
        /// </summary>
        protected RigidBody2D rigidBody2D;
        /// <summary>
        /// Constraint on min car velocity.
        /// </summary>
        protected const float velocityConstraint = 6f;

        /// <summary>
        /// Keeps all car properties.
        /// </summary>
        public CarProps properties;

        /// <summary>
        /// Defines a distance between front and rear wheel.
        /// </summary>
        protected float wheelBase;
        /// <summary>
        /// Responsible for car front wheels steering angle.
        /// </summary>
        protected float steeringAngle;
        /// <summary>
        /// Defines current car direction on scene (in radians).
        /// </summary>
        protected float carDirectionAngle;
        /// <summary>
        /// Keeps a driving mode.
        /// Drive or Reverse.
        /// </summary>
        protected int drivingMode;

        /// <summary>
        /// Defines a position of a front wheel.
        /// </summary>
        protected Vector2 frontWheel;
        /// <summary>
        /// Defines a position of a back wheel.
        /// </summary>
        protected Vector2 backWheel;       

        private float fuelLevel;
        /// <summary>
        /// Responsible for getting and setting current fuel level.
        /// </summary>
        public float FuelLevel
        {
            get => this.fuelLevel;
            set
            {
                if (this.fuelLevel + value >= properties.MaxFuelLevel)
                    this.fuelLevel = properties.MaxFuelLevel;
                else
                    this.fuelLevel = value;
            }
        }
        /// <summary>
        /// String ID name of a car.
        /// </summary>
        public string id;

        private bool[] laps;
        private int beingLocatedOnFinishLine;
        private int _lapsPassed;
        /// <summary>
        /// Responsible for counting laps that were passed.
        /// </summary>
        public int LapsPassed
        {
            get
            {
                var result = 0;
                for (int i = 0; i < this.laps.Length; i++)
                {
                    if (this.laps[i])
                        result++;
                }
                return result;
            }
        }

        #endregion

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
            var handler = EndedRace;
            handler?.Invoke(this, e);
        }


        /// <summary>
        /// Override of FixedUpdate method.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            var steer = MathHelper.DegreesToRadians(steeringAngle);

            Vector2 deltaBackWheel = Vector2.Zero;
            Vector2 deltaFrontWheel = Vector2.Zero;

            deltaBackWheel.X = rigidBody2D.velocity * (float)fixedDeltaTime *
                (float)Math.Cos(carDirectionAngle);
            deltaBackWheel.Y = rigidBody2D.velocity * (float)fixedDeltaTime *
                (float)Math.Sin(carDirectionAngle);

            // Calculations that push the front wheel forward and
            // conserve the wheelBase distance.
            var distance = rigidBody2D.velocity * (float)fixedDeltaTime;
            var B = (wheelBase - distance) * Math.Cos(steer);
            var C = distance * (2 * wheelBase - distance);
            var calc = Math.Sqrt(B * B + C) - B;

            deltaFrontWheel.X = (float)calc *
                (float)Math.Cos(carDirectionAngle + steer);
            deltaFrontWheel.Y = (float)calc *
                (float)Math.Sin(carDirectionAngle + steer);

            backWheel += deltaBackWheel;
            frontWheel += deltaFrontWheel;
            base.Position = (frontWheel + backWheel) / 2;

            // Detect and resolve collisions.
            foreach (var @object in EngineCore.gameObjects.ToList<GameObject>())
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
                                backWheel -= 1.5f * deltaBackWheel;
                                frontWheel -= 1.5f * deltaFrontWheel;
                                rigidBody2D.velocity /= -2.75f;
                            }
                        }
                    }
                }
            }

            base.Position = (frontWheel + backWheel) / 2;
            carDirectionAngle = (float)Math.Atan2(frontWheel.Y - backWheel.Y,
                frontWheel.X - backWheel.X);
            base.Rotation = MathHelper.RadiansToDegrees(carDirectionAngle);

            ApplyFuelConsumprion(fixedDeltaTime);
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
                    if ((this.Rotation < 90f &&
                        this.Rotation > -90f &&
                        rigidBody2D.velocity > 0) ||
                        (this.Rotation > 90f &&
                        this.Rotation < -90f &&
                        rigidBody2D.velocity < 0))
                    {
                        if (!this.laps[_lapsPassed])
                        {
                            this.laps[_lapsPassed] = true;
                        }
                        this.beingLocatedOnFinishLine++;
                    }

                    if (LapsPassed == 5 + 1)
                        OnEndedRace(new GameEventArgs(this));
                }
                else
                {
                    if (e.another is OuterFinishLine)
                    {
                        if (this.beingLocatedOnFinishLine > 0)
                        {
                            this._lapsPassed = LapsPassed;
                            this.beingLocatedOnFinishLine = 0;
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
            foreach (var @object in EngineCore.gameObjects.ToList<GameObject>())
            {
                if (@object is Prize)
                {
                    if (ReferenceEquals(@object, e.another))
                    {
                        ((Prize)@object).PickUp(this);
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
            if (this.fuelLevel <= 0.001)
            {
                // Choose another car as the winner.
                if (this.id == "Black")
                    this.id = "Purple";
                else if (this.id == "Purple")
                    this.id = "Black";
                    
                OnEndedRace(new GameEventArgs(this));
                return;
            }

            var consumptionTime = 5f;
            if (rigidBody2D.velocity == 0)
                this.fuelLevel -= (properties.IdleFuelConsumption * (float)fixedDeltaTime)
                    / consumptionTime;
            else
                this.fuelLevel -= (properties.DrivingFuelConsumption * (float)fixedDeltaTime)
                    / consumptionTime;
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
            if (rigidBody2D.velocity >= properties.MaxVelocity)
                rigidBody2D.velocity = properties.MaxVelocity;
            if (rigidBody2D.velocity <= properties.MaxVelocityReverse)
                rigidBody2D.velocity = properties.MaxVelocityReverse;

            // Fixing a STOP-point.
            if (rigidBody2D.velocity <= velocityConstraint &&
                rigidBody2D.velocity >= -velocityConstraint)
                rigidBody2D.velocity = 0;


            if (InputController.CurrentKeyboardState.IsKeyDown(gas))
            {
                rigidBody2D.engineForce = drivingMode * properties.MaxEngineForce;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(brake))
            {
                if (rigidBody2D.velocity == 0)
                    rigidBody2D.breakingForce = 0;
                else if (rigidBody2D.velocity > 0)
                    rigidBody2D.breakingForce = properties.MaxBreakingForce;
                else
                    rigidBody2D.breakingForce = -properties.MaxBreakingForce;
            }
            else
            {
                rigidBody2D.breakingForce = 0;
                rigidBody2D.engineForce = 0;
            }


            if (InputController.CurrentKeyboardState.IsKeyDown(left))
            {
                this.steeringAngle = -properties.MaxSteeringAngle;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(right))
            {
                this.steeringAngle = properties.MaxSteeringAngle;
            }
            else
            {
                this.steeringAngle = 0f;
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
                drivingMode = (int)DrivingModes.Drive;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(reverse))
            {
                drivingMode = (int)DrivingModes.Reverse;
            }
        }

        #endregion

        /// <summary>
        /// Sets a start car position.
        /// </summary>
        /// <param name="startPosition"></param>
        protected virtual void SetStartCarPosition(Vector2 startPosition)
        {
            base.Position = startPosition;
            this.frontWheel = base.Position +
                this.wheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
            this.backWheel = base.Position -
                this.wheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
        }
    }

    /// <summary>
    /// Defines main driving modes.
    /// </summary>
    public enum DrivingModes
    {
        /// <summary>
        /// Returns -1 cause all forces are in 'negative' direction.
        /// </summary>
        Reverse = -1,
        /// <summary>
        /// Returns 0 cause all forces are in 'neutral' direction.
        /// </summary>
        Neutral = 0,
        /// <summary>
        /// Returns 1 cause all forces are in 'positive' direction.
        /// </summary>
        Drive = 1
    }
}
