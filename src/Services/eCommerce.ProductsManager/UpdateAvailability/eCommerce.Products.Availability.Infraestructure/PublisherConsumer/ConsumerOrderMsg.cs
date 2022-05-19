using eCommerce.Commons.Objects.Messaging;
using eCommerce.Products.Availability.Core.Config;
using eCommerce.Products.Availability.Core.Contracts.Services;
using eCommerce.Products.Availability.Core.Objects.Dtos;
using eCommerce.Products.Availability.Infraestructure.Publisher;
using eCommerce.PublisherSubscriber.Messaging;
using eCommerce.PublisherSubscriber.Object;
using Newtonsoft.Json;

namespace eCommerce.Products.Availability.Infraestructure.PublisherConsumer
{
    internal class ConsumerOrderMsg : ConsumerMessage<OrderMsg>
    {
        private readonly IProductService _productService;

        public ConsumerOrderMsg(IProductService productService)
           : base()
        {
            _productService = productService;
            InitAsDistributedMessage(AppConfiguration.Configuration["AppConfiguration:OrderExchangeName"].ToString());
        }

        public override async Task ProcessMessage(Message<OrderMsg> messsage)
        {
            var strMessage = JsonConvert.SerializeObject(messsage);
            var notification = JsonConvert.DeserializeObject<Message<OrderMsg>>(strMessage);

            if (notification != null)
            {
                var order = notification.BusinessObject;
                var products = order.Products.Select(x => new ProductDto(x.Id, x.Price, x.Units));
                var result = await _productService.UdateProductStock(products);
            }

        }
    }
}
