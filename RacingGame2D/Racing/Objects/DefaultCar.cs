using RGEngine.Input;
using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;


namespace Racing.Objects
{
    public class DefaultCar : Car
    {
        public DefaultCar(string vehicleTexturePath)
            : base(vehicleTexturePath)
        {


            
            // Default values.
            MaxSpeed = 20f;
            MaxSteeringAngle = 30f;
            MaxFuelAmount = 50f;
            MaxAcceleration = new Vector2(3f, 0f);
            WheelBase = 5f;
        }


        public override void FixedUpdate(double deltaTime)
        {
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.W))
                rigidBody2D.acceleration.X = MaxAcceleration.X;
            else
                rigidBody2D.acceleration.X = 0f;

            base.FixedUpdate(deltaTime);
        }
    }
}
