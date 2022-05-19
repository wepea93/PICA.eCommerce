using eCommerce.Commons.ExtensionMethods.DataTableExt;
using eCommerce.Products.Availability.Infraestructure.Models.UnitOfWorks;
using eCommerce.Products.Availability.Core.Contracts.Repositories;
using eCommerce.Products.Availability.Core.Objects.DbTypes;
using eCommerce.Products.Availability.Infraestructure.Contexts.DbProduct;

namespace eCommerce.Products.Availability.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbProductContext _dbcontext;
        private readonly IUnitOfWork _unitOfWork;

        public ProductRepository(DbProductContext dbcontext, IUnitOfWork unitOfWork)
        {
            _dbcontext = dbcontext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductEntity> GetProduct(long productId)
        {
            return await _dbcontext.SpGetProductAsync(productId);
        }

        public async Task<bool> UpdateProduct(ProductEntity productEntity)
        {
            var result = await _dbcontext.SpUpdateProductAsync(productEntity);
            await _unitOfWork.ConfirmAsync();
            return result;
        }

        public async Task<bool> RemoveUnitsToProductStock(IEnumerable<ProductEntity> productEntity)
        {
            var dataTable = productEntity.CopyToDataTable();
            var result = await _dbcontext.SpUpdateProductStockAsync(dataTable);
            await _unitOfWork.ConfirmAsync();
            return result;
        }
    }
}
