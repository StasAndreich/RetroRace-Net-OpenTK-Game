using System;
using OpenTK;
using OpenTK.Input;
using RGEngine.BaseClasses;
using RGEngine.Input;
using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.Support;


namespace Racing.Objects
{
    public abstract class Car : GameObject
    {
        public Car(string vehicleTexturePath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidBody2D = AddComponent<RigidBody2D>();

            var vehicleTexture = ContentLoader.LoadTexture(vehicleTexturePath);
            //var wheelTexture = ContentLoader.LoadTexture(@"");

            var vehicleSprite = new Sprite(vehicleTexture, new Vector2(0.3f, 0.3f),
                new Vector2(0f, 0f), 1);
            //var wheelSprite = new Sprite(wheelTexture, new Vector2(0.3f, 0.3f),
            //    new Vector2(0f, 0f), 0);

            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(vehicleSprite);


            // Set defaults for a default car.
            MaxEngineForce = 400000f;
            MaxVelocity = 520f;
            MaxVelocityReverse = -250f;
            MaxSteeringAngle = 30f;
            MaxSteeringSpeed = 80f;
            MaxBreakingForce = 500000f;

            // Set default wheelBase value and start car position.
            wheelBase = 45f;
            SetStartCarPosition(new Vector2(0f, 0f), wheelBase);

            // Set default RigidBody parameters for a basic Car object.
            rigidBody2D.mass = 1200f;
            rigidBody2D.dragCoefficient = 750f;
        }


        protected float MaxEngineForce { get; set; }
        protected float Mass { get; set; }

        protected float MaxVelocity { get; set; }
        protected float MaxVelocityReverse { get; set; }
        protected float MaxSteeringAngle { get; set; }
        protected float MaxSteeringSpeed { get; set; }
        protected float MaxBreakingForce { get; set; }

        protected float wheelBase;

        protected float steeringAngle;
        protected float carDirectionAngle;
        protected float steeringSpeed;

        protected Vector2 frontWheelPosition;
        protected Vector2 backWheelPosition;

        protected int drivingMode;

        protected float MaxFuelAmount { get; set; }
        protected float FuelLevel { get; set; }

        protected const float minVelocityConstraint = 2.5f;
        protected const float minSteeringAngleConstraint = 1f;

        // GameObject Components.
        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody2D;


        public override void FixedUpdate(double fixedDeltaTime)
        {
            UpdateGearboxState();
            TakeUserInput();

            // Update Steering angle of front car wheels.
            steeringAngle += steeringSpeed * (float)fixedDeltaTime;

            //// Update Back wheels position.
            //backWheelPosition += rigidBody2D.velocity * (float)fixedDeltaTime *
            //    new Vector2((float)Math.Cos(MathHelper.DegreesToRadians(carDirectionAngle)),
            //    (float)Math.Sin(MathHelper.DegreesToRadians(carDirectionAngle)));

            //// Update Front wheels position.
            //frontWheelPosition += rigidBody2D.velocity * (float)fixedDeltaTime *
            //    new Vector2((float)Math.Cos(MathHelper.DegreesToRadians(carDirectionAngle + steeringAngle)),
            //    (float)Math.Sin(MathHelper.DegreesToRadians(carDirectionAngle + steeringAngle)));

            // Update Back wheels position.
            backWheelPosition += rigidBody2D.velocity * (float)fixedDeltaTime *
                new Vector2((float)Math.Cos(carDirectionAngle),
                (float)Math.Sin(carDirectionAngle));

            // Update Front wheels position.
            frontWheelPosition += rigidBody2D.velocity * (float)fixedDeltaTime *
                new Vector2((float)Math.Cos(carDirectionAngle + steeringAngle),
                (float)Math.Sin(carDirectionAngle + MathHelper.DegreesToRadians(steeringAngle)));

            // Update Car fields.
            base.Position = (frontWheelPosition + backWheelPosition) / 2;
            carDirectionAngle = (float) Math.Atan2(frontWheelPosition.Y - backWheelPosition.Y,
                frontWheelPosition.X - backWheelPosition.X);

            // Update car sprites rotation.
            for (int i = 0; i < spriteRenderer.RenderQueue.Quantity; i++)
            {
                spriteRenderer.RenderQueue[i].Rotation = carDirectionAngle;
            }
        }

        protected virtual void TakeUserInput()
        {
            // Check for max and min velocity-values.
            if (rigidBody2D.velocity > MaxVelocity)
                rigidBody2D.velocity = MaxVelocity;
            if (rigidBody2D.velocity < MaxVelocityReverse)
                rigidBody2D.velocity = MaxVelocityReverse;

            // Fixing a STOP-point.
            if (rigidBody2D.velocity >= -minVelocityConstraint &&
                rigidBody2D.velocity <= minVelocityConstraint)
            {
                rigidBody2D.velocity = 0f;
            }

            if (InputController.CurrentKeyboardState.IsKeyDown(Key.W))
            {
                if (rigidBody2D.velocity == 0f)
                    rigidBody2D.engineForce = drivingMode * MaxEngineForce;

                else if (rigidBody2D.velocity > 0f)
                    rigidBody2D.engineForce = drivingMode * MaxEngineForce;

                else
                    rigidBody2D.engineForce = drivingMode * MaxEngineForce;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(Key.S))
            {
                if (rigidBody2D.velocity == 0f)
                    rigidBody2D.breakingForce = 0f;

                else if (rigidBody2D.velocity > 0f)
                    rigidBody2D.breakingForce = MaxBreakingForce;

                else
                    rigidBody2D.breakingForce = -MaxBreakingForce;
            }
            else
            {
                rigidBody2D.breakingForce = 0f;
                rigidBody2D.engineForce = 0f;
            }


            // Update steering.
            if (this.steeringAngle > MaxSteeringAngle)
                this.steeringAngle = MaxSteeringAngle;
            if (this.steeringAngle < -MaxSteeringAngle)
                this.steeringAngle = -MaxSteeringAngle;

            if (this.steeringAngle >= -minSteeringAngleConstraint &&
                this.steeringAngle <= minSteeringAngleConstraint)
            {
                this.steeringAngle = 0f;
            }

            if (InputController.CurrentKeyboardState.IsKeyDown(Key.A))
            {
                this.steeringSpeed = -MaxSteeringSpeed;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(Key.D))
            {
                this.steeringSpeed = MaxSteeringSpeed;
            }
            else
            {
                if (this.steeringAngle == 0f)
                    this.steeringSpeed = 0f;
                else if (this.steeringAngle > 0f)
                    this.steeringSpeed = -MaxSteeringSpeed;
                else
                    this.steeringSpeed = MaxSteeringSpeed;
            }
        }

        protected virtual void UpdateGearboxState()
        {
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.RShift))
            {
                drivingMode = (int)DrivingModes.Neutral;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(Key.RControl))
            {
                drivingMode = (int)DrivingModes.Drive;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(Key.Enter))
            {
                drivingMode = (int)DrivingModes.Reverse;
            }
        }

        protected virtual void SetStartCarPosition(Vector2 startPosition, float carWheelBaseLength)
        {
            base.Position = startPosition;
            this.frontWheelPosition = base.Position +
                carWheelBaseLength / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
            this.backWheelPosition = base.Position -
                carWheelBaseLength / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
        }
    }

    public enum DrivingModes
    {
        /// <summary>
        /// Returns -1 cause all forces are in 'negative' direction.
        /// </summary>
        Reverse = -1,
        Neutral = 0,
        /// <summary>
        /// Returns 1 cause all forces are in 'positive' direction.
        /// </summary>
        Drive = 1
    }
}
