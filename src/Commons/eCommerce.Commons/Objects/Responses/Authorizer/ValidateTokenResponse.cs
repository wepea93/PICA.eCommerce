
namespace eCommerce.Commons.Objects.Responses.Authorizer
{
    public class ValidateTokenResponse
    {
        public bool IsValid { get; set; }

        public ValidateTokenResponse(bool isValid) 
        {
            IsValid = isValid;
        }
    }
}
