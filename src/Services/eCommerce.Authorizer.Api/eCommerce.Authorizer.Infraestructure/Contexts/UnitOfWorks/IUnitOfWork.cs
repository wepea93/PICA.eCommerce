
namespace Authorizer.Infraestructure.Models.UnitOfWorks
{
    public interface IUnitOfWork
    {
        void Confirm();

        Task ConfirmAsync();
    }
}
