using eCommerce.Commons.Utilities;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Commons.Validations.Products
{
    public  class ValidateSort : ValidationAttribute
    {
        public ValidateSort() { }

        public override bool IsValid(object? value)
        {
            try
            {
                var sort = value as string;

                if (string.IsNullOrEmpty(sort)) return false; 

                if (sort == ProductUtilities.ORDERBY.MaxPrice.ToString() || sort == ProductUtilities.ORDERBY.Relevance.ToString()
                    || sort == ProductUtilities.ORDERBY.MinPrice.ToString())
                    return true;

                return false;

            } catch (Exception)
            {
                return false;
            }
        }
    }
}
