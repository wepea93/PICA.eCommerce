using System.ComponentModel.DataAnnotations;


namespace eCommerce.Blazor.Demo.Common.Validations
{
    public class ValidateProductCode : ValidationAttribute
    {
        public ValidateProductCode() { }

        public override bool IsValid(object? value)
        {
            try
            {
                return (long)value > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
