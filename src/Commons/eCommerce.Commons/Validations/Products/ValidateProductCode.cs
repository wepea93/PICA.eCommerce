using System.ComponentModel.DataAnnotations;


namespace eCommerce.Commons.Validations.Products
{
    public class ValidateProductCode : ValidationAttribute
    {
        public ValidateProductCode() { }

        public override bool IsValid(object? value)
        {
            try
            {
                return (long) value > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
