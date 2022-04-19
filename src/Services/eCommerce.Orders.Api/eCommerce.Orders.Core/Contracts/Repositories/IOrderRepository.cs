using eCommerce.Orders.Core.Objects.DbTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Core.Contracts.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> CreateOrder(OrderEntity orderEntity, List<OrderDetailEntity> orderDetailEntity);
        Task<IList<OrderEntity>> GetOrderByIdOrCustomer(string ID, string customer);
        Task<IList<OrderDetailEntity>> GetOrderDetailById(string ID);
    }
}
