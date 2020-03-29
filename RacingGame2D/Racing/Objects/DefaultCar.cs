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
            rigidBody2D.frictionCoefficient = 0.3f;

            MaxVelocity = 130f;
            MaxSteeringAngle = 30f;
            MaxFuelAmount = 50f;
            MaxEngineForceAcceleration = 60f;
        }


        public override void FixedUpdate(double fixedDeltaTime)
        {
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.W))
            {
                if (rigidBody2D.velocity > MaxVelocity)
                {
                    rigidBody2D.velocity = MaxVelocity;
                }
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
