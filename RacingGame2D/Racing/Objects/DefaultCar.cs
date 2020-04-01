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
            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
