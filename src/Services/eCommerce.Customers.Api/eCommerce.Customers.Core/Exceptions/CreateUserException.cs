
namespace eCommerce.Customers.Core.Exceptions
{
    public class CreateUserException : Exception
    {
        public CreateUserException()
            : base("Error al crear el usuario") { }
    }
}
