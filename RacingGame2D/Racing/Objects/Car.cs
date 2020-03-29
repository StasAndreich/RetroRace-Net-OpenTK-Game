using System;
using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.Support;


namespace Racing.Objects
{
    public abstract class Car : GameObject
    {
        public Car(string vehicleTexturePath)
        {
            var vehicleTexture = ContentLoader.LoadTexture(vehicleTexturePath);
            //var wheelTexture = ContentLoader.LoadTexture(@"");

            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidBody2D = AddComponent<RigidBody2D>();

            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(vehicleTexture);

            // Initial values.
            carDirectionAngle = 0;
            steeringAngle = 0;
            WheelBase = 5f;

            
            rigidBody2D.frictionCoefficient = 0.3f;


            // Set the start position.
            base.Position = new Vector2(0f, 0f);
            this.frontWheelPosition = base.Position +
                this.WheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
            this.backWheelPosition = base.Position +
                this.WheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
        }


        protected float MaxVelocity { get; set; }

        protected float MaxEngineForceAcceleration { get; set; }

        protected float MaxSteeringAngle { get; set; }

        protected float WheelBase { get; }

        protected float steeringAngle;

        protected float carDirectionAngle;

        protected Vector2 frontWheelPosition;

        protected Vector2 backWheelPosition;

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
                new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
            frontWheelPosition += rigidBody2D.velocity * (float)fixedDeltaTime *
                new Vector2((float)Math.Cos(carDirectionAngle + steeringAngle), (float)Math.Sin(carDirectionAngle + steeringAngle));

            // Update Car fields.
            base.Position = (frontWheelPosition + backWheelPosition) / 2;
            carDirectionAngle = (float) Math.Atan2(frontWheelPosition.Y - backWheelPosition.Y,
                frontWheelPosition.X - backWheelPosition.X);
        }
    }
}
