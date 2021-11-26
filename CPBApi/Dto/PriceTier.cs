using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPBApi.Dto
{
    public class PriceTier
    {
        public double fromValue { get; set; }
        public double toValue { get; set; }
        public double unitPriceC { get; set; }
    }
}
