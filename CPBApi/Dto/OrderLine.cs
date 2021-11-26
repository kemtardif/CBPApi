using CPBApi.Validations;
using System.Collections.Generic;

namespace CPBApi.Dto
{
    public class OrderLine
    {
        public double Consumption_fx { get; set; }
        public double Amount_fx { get; set; }
        public string Period_fx { get; set; }
        public string Product_fx { get; set; }

        public List<PriceTier> Tiers { get; set; }
    }
}
