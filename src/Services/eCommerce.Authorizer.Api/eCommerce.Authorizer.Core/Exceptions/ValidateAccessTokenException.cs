
namespace Authorizer.Core.Exceptions
{
    public class ValidateAccessTokenException : Exception
    {
        public ValidateAccessTokenException()
            : base("Access token invalido") { }
    }
}
