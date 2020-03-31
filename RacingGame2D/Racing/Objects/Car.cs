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

            MaxSteeringAngle = 30f;
            wheelBase = 15f;

            // Set a start position.
            base.Position = new Vector2(0f, 0f);
            this.frontWheelPosition = base.Position +
                this.wheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
            this.backWheelPosition = base.Position -
                this.wheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
        }


        protected float MaxEngineForce { get; set; }
        protected float Mass { get; set; }

        protected float MaxVelocity { get; set; }
        protected float MaxVelocityReverse { get; set; }
        protected float MaxEngineForceAcceleration { get; set; }
        protected float MaxSteeringAngle { get; set; }

        protected float wheelBase;

        protected float steeringAngle;
        protected float carDirectionAngle;
        protected float steeringConstant;

        protected Vector2 frontWheelPosition;
        protected Vector2 backWheelPosition;

        protected int drivingMode;

        protected float MaxFuelAmount { get; set; }
        protected float FuelLevel { get; set; }


        // GameObject Components.
        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody2D;


        public override void FixedUpdate(double fixedDeltaTime)
        {
            // UPDATE ANIMATION.
            // Get changes over deltaTime.
            backWheelPosition += rigidBody2D.velocity * (float)fixedDeltaTime *
                new Vector2((float)Math.Cos(MathHelper.DegreesToRadians(carDirectionAngle)),
                (float)Math.Sin(MathHelper.DegreesToRadians(carDirectionAngle)));

            frontWheelPosition += rigidBody2D.velocity * (float)fixedDeltaTime *
                new Vector2((float)Math.Cos(MathHelper.DegreesToRadians(carDirectionAngle + steeringAngle)),
                (float)Math.Sin(MathHelper.DegreesToRadians(carDirectionAngle + steeringAngle)));

            // Update Car fields.
            base.Position = (frontWheelPosition + backWheelPosition) / 2;
            carDirectionAngle = MathHelper.RadiansToDegrees((float) Math.Atan2(frontWheelPosition.Y - backWheelPosition.Y,
                frontWheelPosition.X - backWheelPosition.X));


            for (int i = 0; i < spriteRenderer.RenderQueue.Quantity; i++)
            {
                spriteRenderer.RenderQueue[i].Rotation = carDirectionAngle;
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

        protected virtual void UpdateCarSteering(double fixedDeltaTime)
        {
            steeringAngle += steeringConstant * (float)fixedDeltaTime;
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
