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
        public Car(string vehicleTexturePath, string )
        {
            //spriteRenderer = AddComponent<SpriteRenderer>();
            //rigidBody = AddComponent<RigidBody2D>();

            var vehicleTexture = ContentLoader.LoadTexture(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Cars\lambo.png");
            var wheelTexture = ContentLoader.LoadTexture(@"");
            sprite = new Sprite(texture, this);
            rigidBody = new RigidBody2D(this);

            // Initial values.
            carHeading = 0;
            steeringAngle = 0;

            // Set the start position.
            base.Position = new Vector2(0f, 0f);
            this.frontWheelPosition = base.Position +
                this.WheelBase / 2 * new Vector2((float)Math.Cos(carHeading), (float)Math.Sin(carHeading));
            this.backWheelPosition = base.Position +
                this.WheelBase / 2 * new Vector2((float)Math.Cos(carHeading), (float)Math.Sin(carHeading));


            WheelBase = 5f;
        }


        protected float MaxSpeed { get; set; }

        protected Vector2 MaxAcceleration { get; set; }

        protected float MaxSteeringAngle { get; set; }

        protected float WheelBase { get; }

        protected float steeringAngle;

        protected float carHeading;

        protected Vector2 frontWheelPosition;

        protected Vector2 backWheelPosition;

        protected float MaxFuelAmount { get; set; }

        protected float FuelLevel { get; set; }


        // GameObject Components.
        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody2D;


        public override void FixedUpdate(double deltaTime)
        {
            // Update velocity value inside.
            rigidBody2D.CallComponent(deltaTime);
            // UPDATE car POSITION.

            // Get changes over deltaTime.
            backWheelPosition += rigidBody.Velocity * (float)deltaTime *
                new Vector2((float)Math.Cos(carHeading), (float)Math.Sin(carHeading));
            frontWheelPosition += rigidBody.Velocity * (float)deltaTime *
                new Vector2((float)Math.Cos(carHeading + steeringAngle), (float)Math.Sin(carHeading + steeringAngle));

            // Update Car fields.
            base.Position = (frontWheelPosition + backWheelPosition) / 2;
            carHeading = (float) Math.Atan2(frontWheelPosition.Y - backWheelPosition.Y,
                frontWheelPosition.X - backWheelPosition.X);
        }


        public override void Update()
        {
            sprite.Position = this.Position;

            base.Update();
        }
    }
}
