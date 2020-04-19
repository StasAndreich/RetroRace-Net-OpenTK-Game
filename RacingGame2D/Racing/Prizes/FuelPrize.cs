using Racing.Objects;
using System;


namespace Racing.Prizes
{
    public class FuelPrize : Prize
    {
        public FuelPrize()
            : base(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\fuel\fuel1.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\fuel\fuel2.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\fuel\fuel3.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\fuel\fuel4.png")
        {
            //base.rigidBody.OnTriggered += FuelPrize_OnTriggered;
        }

        protected override void ApplyDecorator(Car car)
        {
            car.properties = new FuelProps(car.properties);
        }
    }
}
