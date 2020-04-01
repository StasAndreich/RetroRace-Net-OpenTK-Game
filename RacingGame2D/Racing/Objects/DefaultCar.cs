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
            rigidBody2D.mass = 1200f;
            rigidBody2D.dragCoefficient = 750f;

            MaxEngineForce = 400000f;
            MaxVelocity = 520f;
            MaxVelocityReverse = -250f;

            MaxBreakingForce = 500000f;
            MaxSteeringSpeed = 80f;
        }


        public override void FixedUpdate(double fixedDeltaTime)
        {
            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
