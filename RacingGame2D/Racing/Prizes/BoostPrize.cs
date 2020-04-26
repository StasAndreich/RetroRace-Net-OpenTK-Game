using Racing.Objects;

namespace Racing.Prizes
{
    /// <summary>
    /// Defines a prize that speeds up a car.
    /// </summary>
    public class BoostPrize : Prize
    {
        /// <summary>
        /// Loading textures.
        /// </summary>
        public BoostPrize()
            : base(@"Contents\Animations\Prizes\Boost\nos1.png",
                  @"Contents\Animations\Prizes\Boost\nos2.png",
                  @"Contents\Animations\Prizes\Boost\nos3.png",
                  @"Contents\Animations\Prizes\Boost\nos4.png")
        {

        }

        /// <summary>
        /// Applies a specified decorator to a CarProps.
        /// </summary>
        /// <param name="car"></param>
        protected override void ApplyPrize(Car car)
        {
            // Remove current decorator if there is one.
            car.properties = new CarProps(car);
            car.properties = new BoostProps(car.properties);
        }
    }
}
