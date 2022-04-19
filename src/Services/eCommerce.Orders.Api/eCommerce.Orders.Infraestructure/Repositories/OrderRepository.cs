using eCommerce.Orders.Core.Contracts.Repositories;
using eCommerce.Orders.Core.Objects.DbTypes;
using eCommerce.Orders.Infraestructure.Contexts.DbOrder;
using eCommerce.Orders.Infraestructure.Models.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Orders.Infraestructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly DbOrderContext _dbcontext;
        private readonly IUnitOfWork _unitOfWork;
        public OrderRepository(DbOrderContext dbcontext, IUnitOfWork unitOfWork)
        {
            _dbcontext = dbcontext;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateOrder(OrderEntity orderEntity,List<OrderDetailEntity> orderDetailEntity)
        {
            var result = await _dbcontext.SpCreateAsync(orderEntity, orderDetailEntity);
            await _unitOfWork.ConfirmAsync();
            return result;
        }
        public async Task<IList<OrderEntity>> GetOrderByIdOrCustomer(string ID, string customer)
        {
            var result = await _dbcontext.SpGetOrderByIdOrCustomer(ID, customer);
            await _unitOfWork.ConfirmAsync();
            return result;
        }
        public async Task<IList<OrderDetailEntity>> GetOrderDetailById(string ID)
        {
            var result = await _dbcontext.SpGetOrderDetailById(ID);
            await _unitOfWork.ConfirmAsync();
            return result;
        }
    }
}
