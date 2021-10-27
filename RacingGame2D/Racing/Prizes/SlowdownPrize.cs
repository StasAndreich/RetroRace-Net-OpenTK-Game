using OpenTK;
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
        public SlowdownPrize(Vector2 position)
            : base(position,
                  @"Contents\Animations\Prizes\Slowdown\slow1.png",
                  @"Contents\Animations\Prizes\Slowdown\slow2.png",
                  @"Contents\Animations\Prizes\Slowdown\slow3.png",
                  @"Contents\Animations\Prizes\Slowdown\slow4.png")
        {
        }

        /// <summary>
        /// Applies a specified decorator to a CarProps.
        /// </summary>
        /// <param name="car"></param>
        protected override void ApplyPrize(Car car)
        {
            // Remove current decorator if there is one.
            car.Properties = new CarProps(car);
            car.Properties = new SlowdownProps(car.Properties);
        }
    }
}
