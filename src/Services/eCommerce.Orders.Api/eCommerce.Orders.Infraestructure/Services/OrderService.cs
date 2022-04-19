using eCommerce.Orders.Core.Contracts.Repositories;
using eCommerce.Orders.Core.Contracts.Services;
using eCommerce.Orders.Core.Helpers.Mappers;
using eCommerce.Orders.Core.Objects.Dtos;
using eCommerce.Orders.Infraestructure.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Infraestructure.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;
        private IMapperHelper _mapper;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork,IMapperHelper mapperHelper)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapperHelper;
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
