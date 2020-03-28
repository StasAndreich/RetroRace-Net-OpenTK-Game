using RGEngine.Input;
using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;


namespace Racing.Objects
{
    public class DefaultCar : Car
    {
        public DefaultCar(string vehicleTexturePath, string wheelTexturePath)
            : base(vehicleTexturePath, wheelTexturePath)
        {


            
            // Default values.
            MaxSpeed = 20f;
            MaxSteeringAngle = 30f;
            MaxFuelAmount = 50f;
            MaxAcceleration = new Vector2(3f, 0f);
        }


        public override void FixedUpdate(double deltaTime)
        {
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.W))
                rigidBody.acceleration.X = MaxAcceleration.X;
            else
                rigidBody.acceleration.X = 0f;

            base.FixedUpdate(deltaTime);
        }
    }
}
