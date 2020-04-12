using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Prizes
{
    public class SlowdownPrizeGenerator : PrizeGenerator
    {
        public override Prize GeneratePrize()
        {
            return new SlowdownPrize();
        }
    }
}
