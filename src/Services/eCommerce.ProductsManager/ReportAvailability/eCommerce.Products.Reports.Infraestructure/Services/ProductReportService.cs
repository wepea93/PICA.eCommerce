using eCommerce.Commons.Objects.Messaging;
using eCommerce.Products.Reports.Core.Config;
using eCommerce.Products.Reports.Core.Contracts.Repositories;
using eCommerce.Products.Reports.Core.Contracts.Services;
using eCommerce.Products.Reports.Core.Objects.DbTypes;
using eCommerce.Products.Reports.Core.Objects.Dtos;
using eCommerce.Products.Reports.Core.Publisher;
using eCommerce.PublisherSubscriber.Object;

namespace eCommerce.Products.Reports.Infraestructure.Services
{
    public class ProductReportService : IProductReportService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly PublisherNotificationMsg _publisherNotificationMsg;

        public ProductReportService(IProductRepository productRepository, 
            IShoppingCartRepository shoppingCartRepository, 
            ICustomerRepository customerRepository,
            PublisherNotificationMsg publisherNotificationMsg)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _customerRepository = customerRepository;
            _publisherNotificationMsg = publisherNotificationMsg;
        }

        public async Task SendChangePriceNotifications(IEnumerable<ProductDto> products)
        {
            var result = await GetCustomersToNotifyChangePrice(products);

            if (result != null && result.Any()) 
            {
                foreach (var item in result) 
                {
                    var notification = new NotificationMsg()
                    {
                        Subject = "Cambio de precio, producto: " + item.ProductName,
                        CustomerEmail = item.CustomerEmail,
                        CustomerName = item.CustomerName,
                        Body = "Se informa que se realizao un cambio de precio sobre le producto: " + item.ProductName + " "
                                + ".Precio anterior: " + item.ProductOldPrice.ToString() + ".Nuevo precio: " + item.ProductNewPrice.ToString()
                    };
                    await SendNotification(notification);
                }               
            }
        }

        public async Task SendChangeStockNotifications(IEnumerable<ProductDto> products)
        {
            var result = await GetCustomersToNotifyChangeStock(products);
            
            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    var notification = new NotificationMsg()
                    {
                        Subject = "Producto: " + item.ProductName + ". No disponible",
                        CustomerEmail = item.CustomerEmail,
                        CustomerName = item.CustomerName,
                        Body = "Se informa que se realizao un cambio de precio sobre le producto: " + item.ProductName + " "
                                + "ya no se encuentra disponible."
                    };
                    await SendNotification(notification);
                }
            }
        }

        private Task SendNotification(NotificationMsg notification) 
        {
            var message = new Message<NotificationMsg>(DateTime.Now, notification);
            _publisherNotificationMsg.PublishMessage(message, AppConfiguration.Configuration["AppConfiguration:EmailNotificationQueueName"].ToString());
            return Task.CompletedTask;
        }

        private async Task<IEnumerable<ReportDto>?> GetCustomersToNotifyChangePrice(IEnumerable<ProductDto> products) 
        {
            var productsEntities = products.Select(x => new ProductEntity() { Id = x.ProductId });
            var productsResult = await _productRepository.GetProductsByIdAsync(productsEntities);

            if (productsResult != null && productsResult.Any(x => x.Stock > 0))
            {
                var customersByProductEntities = await _shoppingCartRepository.GetCustomersByProductsIdAsync(productsEntities);

                if (customersByProductEntities != null && customersByProductEntities.Any())
                {
                    var customerEntities = customersByProductEntities.Select(x => new CustomerEntity() { Id = x.CustomerId });
                    var customerResult = await _customerRepository.GetCustomersByIdAsync(customerEntities);

                    if (customerResult != null && customerResult.Any())
                    {
                        return (from productNotif in products
                                      join product in productsResult on productNotif.ProductId equals product.Id
                                      join custByprod in customersByProductEntities on product.Id equals custByprod.ProductId
                                      join customer in customerResult on custByprod.CustomerId equals customer.Id
                                      select new ReportDto()
                                      {
                                          CustomerId = customer.Id,
                                          CustomerEmail = customer.Email,
                                          CustomerName = customer.Name,
                                          ProductName = product.Name,
                                          ProductOldPrice = productNotif.OldPrice,
                                          ProductNewPrice = productNotif.NewPrice
                                      });
                    }
                }
            }
            return null;
        }

        private async Task<IEnumerable<ReportDto>?> GetCustomersToNotifyChangeStock(IEnumerable<ProductDto> products)
        {
            var productsEntities = products.Select(x => new ProductEntity() { Id = x.ProductId });
            var productsResult = await _productRepository.GetProductsByIdAsync(productsEntities);

            if (productsResult != null && productsResult.Any(x => x.Stock <= 0))
            {
                var customersByProductEntities = await _shoppingCartRepository.GetCustomersByProductsIdAsync(productsEntities);

                if (customersByProductEntities != null && customersByProductEntities.Any())
                {
                    var customerEntities = customersByProductEntities.Select(x => new CustomerEntity() { Id = x.CustomerId });
                    var customerResult = await _customerRepository.GetCustomersByIdAsync(customerEntities);

                    if (customerResult != null && customerResult.Any())
                    {
                        return (from product in productsResult
                                      join custByprod in customersByProductEntities on product.Id equals custByprod.ProductId
                                      join customer in customerResult on custByprod.CustomerId equals customer.Id
                                      select new ReportDto()
                                      {
                                          CustomerId = customer.Id,
                                          CustomerEmail = customer.Email,
                                          CustomerName = customer.Name,
                                          ProductName = product.Name
                                      });
                    }
                }
            }
            return null;
        }
    }
}
