using eCommerce.Commons.Objects.Messaging;
using eCommerce.PublisherSubscriber.Messaging;
using eCommerce.PublisherSubscriber.Object;
using eCommerce.ShoppingCart.Core.Config;
using eCommerce.ShoppingCart.Core.Contracts.Services;
using Newtonsoft.Json;

namespace eCommerce.ShoppingCart.Core.Consumer
{
    public class ConsumerOrderMsg : ConsumerMessage<OrderMsg>
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ConsumerOrderMsg(IShoppingCartService shoppingCartService)
           : base()
        {
            _shoppingCartService = shoppingCartService;
            InitAsDistributedMessage(AppConfiguration.Configuration["AppConfiguration:OrderExchangeName"].ToString());
        }

        public override async Task ProcessMessage(Message<OrderMsg> messsage)
        {
            var strMessage = JsonConvert.SerializeObject(messsage);
            var notification = JsonConvert.DeserializeObject<Message<OrderMsg>>(strMessage);

            if (notification != null)
            {
                var order = notification.BusinessObject;
                await _shoppingCartService.DeleteShoppingCartByUser(order.UserId);
            }
        }
    }
}
