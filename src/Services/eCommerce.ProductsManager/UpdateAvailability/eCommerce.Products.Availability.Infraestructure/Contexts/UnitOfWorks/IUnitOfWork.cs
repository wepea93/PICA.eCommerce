
namespace eCommerce.Products.Availability.Infraestructure.Models.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Confirm();

        Task ConfirmAsync();
    }
}
