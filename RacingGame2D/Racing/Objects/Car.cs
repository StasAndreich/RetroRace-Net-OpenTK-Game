using System;
using OpenTK;
using OpenTK.Input;
using RGEngine.BaseClasses;
using RGEngine.Input;
using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.Support;
using RGEngine;
using System.Linq;

namespace Racing.Objects
{
    public abstract class Car : GameObject, ICollidable
    {
        public Car()
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidBody2D = AddComponent<RigidBody2D>();

            // Set defaults for a default car.
            properties = new CarProps(this);
            this.fuelLevel = properties.MaxFuelLevel;

            fuelTimer = new GameTimer(5f);
            fuelTimer.Elapsed += (sender, e) => ApplyFuelConsumprion();

            // Set default wheelBase value and start car position.
            wheelBase = 80f;
            SetStartCarPosition(new Vector2(150f, 150f));

            // Set default RigidBody parameters for a basic Car object.
            rigidBody2D.mass = 1200f;
            rigidBody2D.frictionConst = 750f;

            base.collider = new PolyCollider(this, new Vector2(110f, 45f));
            base.collider.ColliderTriggered += Collider_ColliderTriggered;

            this.laps = new bool[5];
        }

        private void Collider_ColliderTriggered(object sender, CollisionEventArgs e)
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

                    if (LapsPassed == 2)
                        OnEndedRace(new GameEventArgs(this));
                }
                else
                {
                    if (this.beingLocatedOnFinishLine > 0)
                    {
                        this._lapsPassed = LapsPassed;
                        this.beingLocatedOnFinishLine = 0;
                    }
                }
            }
        }

        // GameObject Components.
        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody2D;
        protected const float velocityConstraint = 6f;

        public CarProps properties { get; set; }

        protected float wheelBase;
        protected float steeringAngle;
        protected float carDirectionAngle;
        protected int drivingMode;

        protected Vector2 frontWheel;
        protected Vector2 backWheel;       

        private float fuelLevel;
        protected float FuelLevel
        {
            get => this.fuelLevel;
            set
            {
                if (this.fuelLevel + value > properties.MaxFuelLevel)
                    this.fuelLevel = properties.MaxFuelLevel;
                else
                    this.fuelLevel = value;
            }
        }
        private GameTimer fuelTimer;
        public string id;

        private bool[] laps;
        private int beingLocatedOnFinishLine;
        private int _lapsPassed;
        protected int LapsPassed
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
            private set
            { 
            }
        }

        public event EventHandler<GameEventArgs> EndedRace;

        public void OnEndedRace(GameEventArgs e)
        {
            var handler = EndedRace;
            handler?.Invoke(this, e);
        }


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

            foreach (var @object in EngineCore.gameObjects.ToList<GameObject>())
            {
                if (@object is ICollidable)
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

            spriteRenderer.RenderQueue[0].Rotation = MathHelper.RadiansToDegrees(carDirectionAngle);

            fuelTimer.Update(fixedDeltaTime);
            // Additional fuel from prizes.
            this.fuelLevel += properties.FuelFillUp;
        }

        protected void ApplyFuelConsumprion()
        {
            if (this.fuelLevel <= 0.001)
            {
                OnEndedRace(new GameEventArgs(this));
                return;
            }

            if (rigidBody2D.velocity == 0)
                this.fuelLevel -= properties.IdleFuelConsumption;
            else
                this.fuelLevel -= properties.DrivingFuelConsumption;
        }

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

        protected virtual void SetStartCarPosition(Vector2 startPosition)
        {
            base.Position = startPosition;
            this.frontWheel = base.Position +
                this.wheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
            this.backWheel = base.Position -
                this.wheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
        }
    }

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
