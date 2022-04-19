using eCommerce.Commons.Utilities;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Validations.Products
{
    public class ValidateProductCondition : ValidationAttribute
    {
        public ValidateProductCondition() { }

        public override bool IsValid(object? value)
        {
            try
            {
                var condition = value.ToString().Split(",");

                if (condition == null) return false;

                if (condition.Any(x=> string.IsNullOrEmpty(x))) return false;

                if (condition.Any(x => x == ProductUtilities.CONDITION.Used.ToString()
                    || x == ProductUtilities.CONDITION.Returned.ToString()
                    || x == ProductUtilities.CONDITION.New.ToString()))
                    return true;

                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
