
using eCommerce.Orders.Core.Objects.DbTypes;
using eCommerce.Orders.Core.Objects.Dtos;
using eCommerce.Orders.Core.Objects.Responses;
namespace eCommerce.Orders.Core.Helpers.Mappers
{
    public class MapperHelper : IMapperHelper
    {
        public IList<OrderSearchDto> MappToOrderDto(IList<OrderEntity> orderEntity)
        {
            if(orderEntity == null)
                return null;
            var orderSearchDto = new List<OrderSearchDto>();
            foreach (var item in orderEntity)
            {
                orderSearchDto.Add(new OrderSearchDto
                {
                    OrderID = item.OrderID,
                    Customer = item.Customer,
                    DateRequiered = item.DateRequiered,
                    DateShipped = item.DateShipped,
                    OrderDate = item.OrderDate,
                    Comment = item.Comment,
                    Status = item.Status
                });
            }
            return orderSearchDto;
        }
        public IList<OrderResponse> MappToOrderResponse(IList<OrderSearchDto> orderSearchDto)
        {
            if (orderSearchDto == null)
                return null;

            var orderResponse = new List<OrderResponse>();
            foreach (var item in orderSearchDto)
            {
                orderResponse.Add(new OrderResponse
                {
                    OrderID = item.OrderID,
                    Customer = item.Customer,
                    DateRequiered = item.DateRequiered,
                    DateShipped = item.DateShipped,
                    OrderDate = item.OrderDate,
                    Comment = item.Comment,
                    Status = item.Status
                });
            }
            return orderResponse;
        }
        public IList<OrderDetailDto> MappToOrderDetailDto(IList<OrderDetailEntity> orderDetailEntity)
        {
            if (orderDetailEntity == null)
                return null;
            var orderDetailDto = new List<OrderDetailDto>();
            foreach (var item in orderDetailEntity)
            {
                orderDetailDto.Add(new OrderDetailDto
                {
                    ProductID=item.ProductID,
                    QuantityOrdered=item.QuantityOrdered,
                    OrderLine = item.OrderLine,
                    PriceEach=item.PriceEach
                });
            }
            return orderDetailDto;
        }
        public IList<OrderDetailResponse> MappToOrderDetailResponse(IList<OrderDetailDto> orderDetailDto)
        {
            if (orderDetailDto == null)
                return null;

            var orderDetailResponse = new List<OrderDetailResponse>();
            foreach (var item in orderDetailDto)
            {
                orderDetailResponse.Add(new OrderDetailResponse
                {
                    ProductID = item.ProductID,
                    QuantityOrdered = item.QuantityOrdered,
                    OrderLine = item.OrderLine,
                    PriceEach = item.PriceEach
                });
            }
            return orderDetailResponse;
        }
    }
}
