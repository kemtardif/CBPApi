using CPBApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace CPBApi.Dto
{
    public class PriceTierModelBinding
    {
        public string productId { get; set; }
        [Required]
        [GreaterThanZero]
        public double fromValue { get; set; }
        [Required]
        [GreaterThanZero]
        public double toValue { get; set; }
        [Required]
        [GreaterThanZero]
        public double unitPriceC { get; set; }

        public PriceTier GetPriceTier() => new()
            { 
                fromValue = fromValue,
                toValue = toValue,
                unitPriceC = unitPriceC
            };

        
    }
}
