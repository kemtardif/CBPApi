using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CPBApi.Validations;

namespace CPBApi.Dto
{
    public class OrderLineModelBinding
    {
        [Required]
        [GreaterThanZero]
        public double Consumption_fx { get; set; }

        public string Period_fx { get; set; }

        [Required]
        [StringLength(255)]
        public string Product_fx { get; set; }

        public List<PriceTierModelBinding> Tiers { get; set; }

        public OrderLine GetOrderLine()
        {
            var orderLine = new OrderLine()
            {
                Consumption_fx = Consumption_fx,
                Period_fx = Period_fx,
                Product_fx = Product_fx,
                Amount_fx = 0,
                Tiers = new List<PriceTier>()              
            };

            foreach(PriceTierModelBinding tier in Tiers)
            {
                orderLine.Tiers.Add(tier.GetPriceTier());
            }

            return orderLine;
        }
    }
}
