using System.ComponentModel.DataAnnotations;

namespace CPBApi.Validations
{
    public class GreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool notNull = value != null;
            bool isDouble = 
                double.TryParse(value.ToString(), out double i);
            return notNull && isDouble && i >= 0;
        }
    }
}
