using Racing.Objects;


namespace Racing.Prizes
{
    public class BoostPrize : Prize
    {
        public BoostPrize()
            : base(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\nos\nos1.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\nos\nos2.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\nos\nos3.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\nos\nos4.png")
        {

        }

        protected override void ApplyDecorator(Car car)
        {
            car.properties = new BoostProps(car.properties);
        }
    }
}
