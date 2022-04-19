using eCommerce.Orders.Core.Objects.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Core.Contracts.Services
{
    public interface IOrderService
    {
        Task<bool> Create(OrderDto Order);
        Task<IList<OrderSearchDto>> GetOrderByIdOrCustomer(string ID, string customer);
        Task<IList<OrderDetailDto>> GetOrderDetailById(string ID);

    }
}
