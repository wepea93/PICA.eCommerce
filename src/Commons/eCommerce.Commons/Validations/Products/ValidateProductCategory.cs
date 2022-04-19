using System.ComponentModel.DataAnnotations;


namespace eCommerce.Commons.Validations.Products
{
    public class ValidateProductCategory : ValidationAttribute
    {
        public ValidateProductCategory() { }

        public override bool IsValid(object? value)
        {
            try
            {
                var categoryId = Convert.ToInt32(value);
                return categoryId > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
