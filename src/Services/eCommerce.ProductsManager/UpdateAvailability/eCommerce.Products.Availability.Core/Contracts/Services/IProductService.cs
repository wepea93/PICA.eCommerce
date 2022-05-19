using eCommerce.Products.Availability.Core.Objects.Dtos;

namespace eCommerce.Products.Availability.Core.Contracts.Services
{
    public interface IProductService
    {
        Task<bool> UpdateProduct(ProductDto product);
        Task<bool> UdateProductStock(IEnumerable<ProductDto> products);
    }
}
