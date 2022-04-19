
namespace eCommerce.ShoppingCart.Infraestructure.Contexts.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Confirm();

        Task ConfirmAsync();
    }
}
