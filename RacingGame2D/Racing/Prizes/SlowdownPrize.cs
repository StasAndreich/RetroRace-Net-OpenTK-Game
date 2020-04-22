using Racing.Objects;

namespace Racing.Prizes
{
    /// <summary>
    /// Defines an object that slows down a car.
    /// </summary>
    public class SlowdownPrize : Prize
    {
        /// <summary>
        /// Loading textures.
        /// </summary>
        public SlowdownPrize()
            : base(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\slow\slow1.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\slow\slow2.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\slow\slow3.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\slow\slow4.png")
        {
        }

        /// <summary>
        /// Applies a specified decorator to a CarProps.
        /// </summary>
        /// <param name="car"></param>
        protected override void ApplyDecorator(Car car)
        {
            car.properties = new SlowdownProps(car.properties);
        }
    }
}
