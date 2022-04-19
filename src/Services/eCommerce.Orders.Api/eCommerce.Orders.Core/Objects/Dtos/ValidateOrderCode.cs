using System.ComponentModel.DataAnnotations;


namespace eCommerce.Orders.Core.Objects.Dtos
{
    public class ValidateOrderCode : ValidationAttribute
    {
        public ValidateOrderCode() { }

        public override bool IsValid(object? value)
        {
            try
            {
                var a = value;
                if (a == null)
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
