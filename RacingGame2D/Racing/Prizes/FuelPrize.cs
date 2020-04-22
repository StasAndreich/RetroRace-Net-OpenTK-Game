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
            : base(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\fuel\fuel1.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\fuel\fuel2.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\fuel\fuel3.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\fuel\fuel4.png")
        {
        }

        /// <summary>
        /// Applies a specified decorator to a CarProps.
        /// </summary>
        /// <param name="car"></param>
        protected override void ApplyDecorator(Car car)
        {
            car.properties = new FuelProps(car.properties);
        }
    }
}
