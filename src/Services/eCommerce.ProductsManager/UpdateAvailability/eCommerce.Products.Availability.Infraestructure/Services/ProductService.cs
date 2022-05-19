using eCommerce.Commons.Objects.Messaging;
using eCommerce.Products.Availability.Core.Config;
using eCommerce.Products.Availability.Core.Contracts.Repositories;
using eCommerce.Products.Availability.Core.Contracts.Services;
using eCommerce.Products.Availability.Core.Objects.DbTypes;
using eCommerce.Products.Availability.Core.Objects.Dtos;
using eCommerce.Products.Availability.Core.Publisher;
using eCommerce.PublisherSubscriber.Object;

namespace eCommerce.Products.Availability.Infraestructure.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly PublisherProductMsg _publisherProducts;

        public ProductService(IProductRepository productRepository, PublisherProductMsg publisherProducts)
        {
            _productRepository = productRepository;
            _publisherProducts = publisherProducts;
        }

        public async Task<bool> UpdateProduct(ProductDto product)
        {
            var productActuality = await _productRepository.GetProduct(product.Id);
            var result = await _productRepository.UpdateProduct(new ProductEntity(product.Id, product.Price, product.Stock));

            if (result && product.Price > 0)
            {
                var productMsg = new ProductMsg(product.Id, productActuality.Price, product.Price, product.Stock);
                var productsMessage = new Message<IEnumerable<ProductMsg>>(DateTime.Now, new List<ProductMsg>() { productMsg });
                var queueName = AppConfiguration.Configuration["AppConfiguration:ReportProductsPriceQueueName"].ToString();
                _publisherProducts.PublishMessage(productsMessage, queueName);
            }
            if (result && product.Stock > 0)
            {
                await PublisheProductsStockMessage(new List<ProductDto>() { product });
            }

            return result;
        }

        public async Task<bool> UdateProductStock(IEnumerable<ProductDto> products)
        {
            var productEntities = products.Select(x => new ProductEntity(x.Id, x.Price, x.Stock));
            var result = await _productRepository.RemoveUnitsToProductStock(productEntities);

            if (result)
            {
                await PublisheProductsStockMessage(products);
            }

            return result;
        }

        private Task PublisheProductsStockMessage(IEnumerable<ProductDto> products) 
        {
            var productsMsg = products.Select(x=> new ProductMsg(x.Id, 0, 0, x.Stock));
            var message = new Message<IEnumerable<ProductMsg>>(DateTime.Now, productsMsg);
            var queueName = AppConfiguration.Configuration["AppConfiguration:ReportProductsStockQueueName"].ToString();
            _publisherProducts.PublishMessage(message, queueName);
            return Task.CompletedTask;
        }
    }
}
