
using eCommerce.Orders.Core.Objects.DbTypes;
using eCommerce.Orders.Core.Objects.Dtos;
using eCommerce.Orders.Core.Objects.Responses;

namespace eCommerce.Orders.Core.Helpers.Mappers
{
    public interface IMapperHelper
    {
        IList<OrderSearchDto> MappToOrderDto(IList<OrderEntity> orderEntity);
        IList<OrderResponse> MappToOrderResponse(IList<OrderSearchDto> orderSearchDto);
        IList<OrderDetailDto> MappToOrderDetailDto(IList<OrderDetailEntity> orderDetailEntity);
        IList<OrderDetailResponse> MappToOrderDetailResponse(IList<OrderDetailDto> orderDetailDto);

    }
}
