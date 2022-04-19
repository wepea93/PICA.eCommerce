using eCommerce.Commons.Objects.Responses.Products;
using Products.Core.Config;
using Products.Core.Objects.DbTypes;
using Products.Core.Objects.Dtos;


namespace Products.Core.Helpers.Mappers
{
    public class MapperHelper : IMapperHelper
    {
        public ProductDto MappToProductDto(ProductEntity productEntity, bool reviews = true) 
        {
            var defaultImage = AppConfiguration.Configuration["AppConfiguration:ImageNotFound"].ToString();
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
                Stock = productEntity.Stock,
                Score = productEntity.SCore, //productEntity.ProductReviews.Count() > 0 ? _productReviewsHelper.CalculateScore(productEntity.ProductReviews.ToList()),
                OtherImages = productEntity.OtherImages != null && productEntity.OtherImages.Any() ?  
                    productEntity.OtherImages.Select(x => x.Image).ToList() :
                    new List<string>() { defaultImage, defaultImage, defaultImage, defaultImage },
            };

            if (reviews && productEntity.ProductReviews != null && productEntity.ProductReviews.Any())
                productDto.Reviews = productEntity.ProductReviews.Select(y => new ProductReviewDto()
                {
                    Id = y.Id,    
                    UserId = y.UserId,
                    Review = y.Review,
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
                OtherImages = productDto.OtherImages,
                Specifications = productDto.Specifications.Select(y => new ProductSpecificationResponse()
                {
                    Description = y.Description,
                    Value = y.Value,
                }),
                Score = productDto.Score == 0 ? "0" : productDto.Score.ToString("#.#"),
                Stock = productDto.Stock
            };

            if (productDto.Reviews != null && productDto.Reviews.Any())
                productResponse.Reviews = productDto.Reviews.Select(y => new ProductReviewResponse()
                {
                    Id = y.Id,
                    UserId = y.UserId,
                    Review = y.Review,
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
