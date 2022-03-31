using Products.Core.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Products.Core.Validations
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

                if (sort == UtilitiesHelper.ORDERBY.MaxPrice.ToString() || sort == UtilitiesHelper.ORDERBY.Relevance.ToString()
                    || sort == UtilitiesHelper.ORDERBY.MinPrice.ToString())
                    return true;

                return false;

            } catch (Exception)
            {
                return false;
            }
        }
    }
}
