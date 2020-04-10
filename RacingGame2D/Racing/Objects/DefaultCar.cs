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

        }


        public override void FixedUpdate(double fixedDeltaTime)
        {
            UpdateGearboxState(Key.Q, Key.E);
            TakeUserInput(Key.W, Key.S, Key.A, Key.D);

            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
