using eCommerce.Commons.Objects.Messaging;
using eCommerce.Products.Reports.Core.Config;
using eCommerce.Products.Reports.Core.Contracts.Services;
using eCommerce.Products.Reports.Core.Objects.Dtos;
using eCommerce.PublisherSubscriber.Messaging;
using eCommerce.PublisherSubscriber.Object;
using Newtonsoft.Json;

namespace eCommerce.Products.Reports.Core.PublisherConsumer
{
    public class ConsumerProductStockMsg : ConsumerMessage<IEnumerable<ProductMsg>>
    {
        private readonly IProductReportService _productReportService;

        public ConsumerProductStockMsg(IProductReportService productReportService)
           : base()
        {
            _productReportService = productReportService;
            InitAsPublishedMessage(AppConfiguration.Configuration["AppConfiguration:ReportProductsStockQueueName"].ToString());
        }

        public override async Task ProcessMessage(Message<IEnumerable<ProductMsg>> messsage)
        {
            var strMessage = JsonConvert.SerializeObject(messsage);
            var notification = JsonConvert.DeserializeObject<Message<IEnumerable<ProductMsg>>>(strMessage);

            if (notification != null)
            {
                var products = notification.BusinessObject;
                var productsDto = products.Select(x => new ProductDto(x.Id));
                await _productReportService.SendChangeStockNotifications(productsDto);
            }
        }
    }
}
