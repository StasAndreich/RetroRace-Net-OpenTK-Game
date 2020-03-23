using RGEngine.Input;
using OpenTK.Input;


namespace Racing.Objects
{
    public class DefaultCar : Car
    {
        public DefaultCar()
        {


            
            // Default values.
            MaxSpeed = 20f;
            MaxSteeringAngle = 30f;
            MaxFuelAmount = 50f;
        }


        public override void FixedUpdate(double deltaTime)
        {
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.W))


            base.FixedUpdate(deltaTime);
        }
    }
}
