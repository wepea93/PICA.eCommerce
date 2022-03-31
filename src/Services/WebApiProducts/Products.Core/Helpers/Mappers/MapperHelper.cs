
using Products.Core.Helpers.ProductReview;
using Products.Core.Objects.DbTypes;
using Products.Core.Objects.Dtos;
using Products.Core.Objects.Responses;

namespace Products.Core.Helpers.Mappers
{
    public class MapperHelper : IMapperHelper
    {
        private readonly IProductReviewsHelper _productReviewsHelper;
        public MapperHelper(IProductReviewsHelper productReviewsHelper) 
        {
            _productReviewsHelper = productReviewsHelper;
        }

        public ProductDto MappToProductDto(ProductEntity productEntity, bool reviews = true) 
        {
            var productDto = new ProductDto
            {
                Code = productEntity.Id,
                Name = productEntity.Name,
                Image = productEntity.Image,
                Price = productEntity.Price,
                Category = new ProductCategoryDto()
                {
                    Id = productEntity.ProductCategoy.Id,
                    Category = productEntity.ProductCategoy.Category,
                },
                Provider = new ProductProviderDto()
                {
                    Id = productEntity.ProductProvider.Id,
                    Provider = productEntity.ProductProvider.Provider,
                },
                Detail = productEntity.ProductDetails.Select(x => x.Description).ToList(),
                Specifications = productEntity.ProductSpecifications.Select(y => new ProductSpecificationDto()
                {
                    Description = y.Description,
                    Value = y.Value,
                }),
                Score = _productReviewsHelper.CalculateScore(productEntity.ProductReviews.ToList())
            };

            if (reviews && productEntity.ProductReviews != null && productEntity.ProductReviews.Any())
                productDto.Reviews = productEntity.ProductReviews.Select(y => new ProductReviewDto()
                {
                    UserId = y.UserId,
                    UserName = y.UserName,
                    Value = y.Value,
                    CreatedAt = y.CreatedAt
                });

            return productDto;
        }

        public IEnumerable<ProductDto> MappToProductDto(IEnumerable<ProductEntity> productEntityList)
        {
            return productEntityList.Select(x => MappToProductDto(x, false));
        }

        public ProductResponse MappToProductResponse(ProductDto productDto)
        {
            if (productDto == null) return null;

            var productResponse = new ProductResponse
            {
                Code = productDto.Code,
                Name = productDto.Name,
                Image = productDto.Image,
                Price = productDto.Price,
                Category = new ProductCategoryResponse()
                {
                    Id = productDto.Category.Id,
                    Category = productDto.Category.Category,
                },
                Provider = new ProductProviderResponse()
                {
                    Id = productDto.Provider.Id,
                    Provider = productDto.Provider.Provider,
                },
                Description = productDto.Detail,
                Specifications = productDto.Specifications.Select(y => new ProductSpecificationResponse()
                {
                    Description = y.Description,
                    Value = y.Value,
                }),
                Score = productDto.Score == 0 ? "0" : productDto.Score.ToString("#.#")
            };

            if (productDto.Reviews != null && productDto.Reviews.Any())
                productResponse.Reviews = productDto.Reviews.Select(y => new ProductReviewResponse()
                {
                    UserId = y.UserId,
                    UserName = y.UserName,
                    Value = y.Value,
                    CreatedAt = y.CreatedAt
                });

            return productResponse;
        }

        public IEnumerable<ProductResponse> MappToProductResponse(IEnumerable<ProductDto> productDtoList)
        {
            if (productDtoList == null) return null;
            return productDtoList.Select(x => MappToProductResponse(x));
        }
    }
}
