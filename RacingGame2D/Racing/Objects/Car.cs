using System;
using OpenTK;
using RGEngine.BaseClasses;


namespace Racing.Objects
{
    public abstract class Car : GameObject
    {
        public Car()
        {
            // Initial values.
            // Car's wheels are meant to be straight at the moment.
            carHeading = 0;
            steeringAngle = 0;

            // Set the start position.
            base.Position = new Vector2(0f, 0f);
            this.frontWheelPosition = base.Position +
                this.WheelBase / 2 * new Vector2((float)Math.Cos(carHeading), (float)Math.Sin(carHeading));
            this.backWheelPosition = base.Position +
                this.WheelBase / 2 * new Vector2((float)Math.Cos(carHeading), (float)Math.Sin(carHeading));
            
            // TEMP !
            // Default values.
            MaxSpeed = 20f;
            MaxSteeringAngle = 30f;
            WheelBase = 5f;
        }

        protected float MaxSpeed { get; set; }

        protected float MaxSteeringAngle { get; set; }

        protected float WheelBase { get; }

        protected float steeringAngle;

        protected float carHeading;

        protected Vector2 frontWheelPosition;

        protected Vector2 backWheelPosition;


        public void Update()
        {
            // Get changes over deltaTime.
            // 5f HERE IS TEMP VELOCITY FROM THE RIGID BODY COMPONENT.
            backWheelPosition += 5f * 

            // Update Car fields.
            base.Position = (frontWheelPosition + backWheelPosition) / 2;
            carHeading = (float) Math.Atan2(frontWheelPosition.Y - backWheelPosition.Y,
                frontWheelPosition.X - backWheelPosition.X);
        }
    }
}
