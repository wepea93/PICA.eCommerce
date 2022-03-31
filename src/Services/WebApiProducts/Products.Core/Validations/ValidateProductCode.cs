using System.ComponentModel.DataAnnotations;


namespace Products.Core.Validations
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
