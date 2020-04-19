using Racing.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Prizes
{
    public class SlowdownPrize : Prize
    {
        public SlowdownPrize()
            : base(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\slow\slow1.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\slow\slow2.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\slow\slow3.png",
                  @"C:\Users\smedy\OneDrive\C4D\retro\launcher\animations\slow\slow4.png")
        {
        }

        protected override void ApplyDecorator(Car car)
        {
            car.properties = new SlowdownProps(car.properties);
        }
    }
}
