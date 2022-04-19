using eCommerce.Commons.Objects.Responses.Products;
using Products.Core.Objects.DbTypes;
using Products.Core.Objects.Dtos;

namespace Products.Core.Helpers.Mappers
{
    public interface IMapperHelper
    {
        ProductDto MappToProductDto(ProductEntity productEntity, bool reviews = true);
        IEnumerable<ProductDto> MappToProductDto(IEnumerable<ProductEntity> productEntityList);
        ProductResponse MappToProductResponse(ProductDto productDto);
        IEnumerable<ProductResponse> MappToProductResponse(IEnumerable<ProductDto> productDtoList);
    }
}
