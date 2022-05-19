using eCommerce.Products.Reports.Core.Objects.Dtos;

namespace eCommerce.Products.Reports.Core.Contracts.Services
{
    public interface IProductReportService
    {
        Task SendChangePriceNotifications(IEnumerable<ProductDto> products);
        Task SendChangeStockNotifications(IEnumerable<ProductDto> products);
    }
}
