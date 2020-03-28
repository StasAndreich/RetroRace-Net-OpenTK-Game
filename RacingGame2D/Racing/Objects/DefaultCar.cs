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
            rigidBody2D.maxVelocity = 130f;
            rigidBody2D.frictionCoefficient = 0.3f;


            MaxSteeringAngle = 30f;
            MaxFuelAmount = 50f;
            MaxEngineForceAcceleration = 8f;
            
        }


        public override void FixedUpdate(double fixedDeltaTime)
        {
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.W))
            {
                rigidBody2D.maxEngineForceAcceleration = MaxEngineForceAcceleration;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(Key.S))
            {
                rigidBody2D.breakingForceCoefficient = 0.4f;
            }
            else
            {
                rigidBody2D.maxEngineForceAcceleration = 0f;
                rigidBody2D.breakingForceCoefficient = 0f;
            }


            if (InputController.CurrentKeyboardState.IsKeyDown(Key.A))
            {

            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(Key.D))
            {

            }
            else
            {

            }

            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
