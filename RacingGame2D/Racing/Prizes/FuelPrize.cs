using Racing.Objects;

namespace Racing.Prizes
{
    /// <summary>
    /// Defines a prize that refuels a car.
    /// </summary>
    public class FuelPrize : Prize
    {
        /// <summary>
        /// Loading textures.
        /// </summary>
        public FuelPrize()
            : base(@"Contents\Animations\Prizes\Fuel\fuel1.png",
                  @"Contents\Animations\Prizes\Fuel\fuel2.png",
                  @"Contents\Animations\Prizes\Fuel\fuel3.png",
                  @"Contents\Animations\Prizes\Fuel\fuel4.png")
        {
        }

        /// <summary>
        /// Adds more fuel to a Car object.
        /// </summary>
        /// <param name="car"></param>
        protected override void ApplyPrize(Car car)
        {
            car.FuelLevel += 7f;
        }
    }
}
