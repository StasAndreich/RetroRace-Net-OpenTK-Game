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
            : base(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\nos\nos1.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\nos\nos2.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\nos\nos3.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\nos\nos4.png")
        {

        }

        /// <summary>
        /// Applies a specified decorator to a CarProps.
        /// </summary>
        /// <param name="car"></param>
        protected override void ApplyDecorator(Car car)
        {
            car.properties = new BoostProps(car.properties);
        }
    }
}
