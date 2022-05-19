using eCommerce.Commons.Objects.Messaging;
using eCommerce.Orders.Core.Config;
using eCommerce.Orders.Core.Contracts.Repositories;
using eCommerce.Orders.Core.Contracts.Services;
using eCommerce.Orders.Core.Helpers.Mappers;
using eCommerce.Orders.Core.Objects.Dtos;
using eCommerce.Orders.Core.Publisher;
using eCommerce.Orders.Infraestructure.Models.UnitOfWorks;
using eCommerce.PublisherSubscriber.Object;

namespace eCommerce.Orders.Infraestructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperHelper _mapper;
        private readonly PublisherOrderMsg _publisherOrderMsg;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork,IMapperHelper mapperHelper, 
            PublisherOrderMsg publisherOrderMsg)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapperHelper;
            _publisherOrderMsg = publisherOrderMsg;
        }

        public async Task<bool> Create(OrderDto Order)
        {
            var orderDetailEntity = new List<Core.Objects.DbTypes.OrderDetailEntity>();
            foreach (var item in Order.OrderDetail)
            {
                orderDetailEntity.Add(new Core.Objects.DbTypes.OrderDetailEntity(item.ProductID, item.QuantityOrdered, item.PriceEach, item.OrderLine));
            }
            var orderEntity = new Core.Objects.DbTypes.OrderEntity(Order.OrderID, Order.OrderDate, Order.DateRequiered, Order.Comment, Order.Customer, "1");
            var result = await _orderRepository.CreateOrder(orderEntity,orderDetailEntity);
            await _unitOfWork.ConfirmAsync();

            if (result) 
            {
                var orderMsg = new OrderMsg()
                { 
                    UserId = Order.Customer,
                    Products = Order.OrderDetail.Select(x=> new ProductMsg(Convert.ToInt64(x.ProductID)))
                };
                var message = new Message<OrderMsg>(DateTime.Now, orderMsg);
                _publisherOrderMsg.DistributeMessage(message, AppConfiguration.Configuration["AppConfiguration:OrderExchangeName"].ToString());
            }
            return result;
        }
        public async Task<IList<OrderSearchDto>> GetOrderByIdOrCustomer(string ID, string customer)
        {
            var order = await _orderRepository.GetOrderByIdOrCustomer(ID,customer);
            var result = order != null ? _mapper.MappToOrderDto(order) : null;
            return result;
        }
        public async Task<IList<OrderDetailDto>> GetOrderDetailById(string ID)
        {
            var orderDetail = await _orderRepository.GetOrderDetailById(ID);
            var result = orderDetail != null ? _mapper.MappToOrderDetailDto(orderDetail) : null;
            return result;
        }
    }
}
