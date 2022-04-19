using eCommerce.Commons.Objects.Responses.Products;
using Products.Core.Objects.Dtos;
using System;
using System.Collections.Generic;

namespace WebApiProductsTest
{
    public static class ProductTestHelper
    {
        public static ProductDto GetProductDto(long producCode)
        {
            return new ProductDto()
            {
                Code = producCode,
                Image = "https://localhost/image",
                Name = "Producto de prueba",
                Category = new ProductCategoryDto(1, "Categoria de prodcuto 1", "https://localhost/image"),
                Provider = new ProductProviderDto(1, "Proveedor de producto 1"),
                Detail = new List<string>() { "Detalle 1", "Detalle 2" },
                Score = 4,
                Price = 200000,
                Specifications = new List<ProductSpecificationDto>()
                {
                    new ProductSpecificationDto("Especificación 1", " valor 1"),
                    new ProductSpecificationDto("Especificación 2", " valor 2")
                },
                Reviews = new List<ProductReviewDto>()
                 {
                    new ProductReviewDto(producCode, Guid.NewGuid().ToString(), "Usuario 1", "Review 1", 4 ),
                    new ProductReviewDto(producCode, Guid.NewGuid().ToString(), "Usuario 2", "Review 2", 4 )
                 }                 
            };
        }

        public static IList<ProductDto> GetProductsDto(long producCode1, long producCode2)
        {
            return new List<ProductDto>() { GetProductDto(producCode1), GetProductDto(producCode2) };
        }

        public static ProductResponse GetProductResponse(long producCode)
        {
            return new ProductResponse() 
            {
                Code = producCode,
                Image = "https://localhost/image",
                Name = "Producto de prueba",
                Category = new ProductCategoryResponse(1, "Categoria de prodcuto 1", "https://localhost/image"),
                Provider = new ProductProviderResponse(1, "Proveedor de producto 1"),
                Description = new List<string>() { "Detalle 1", "Detalle 2" },
                Score = "4",
                Price = 200000,
                Specifications = new List<ProductSpecificationResponse>()
                {
                    new ProductSpecificationResponse(){ Description = "Especificación 1", Value = " valor 1" },
                    new ProductSpecificationResponse(){ Description = "Especificación 2", Value = " valor 2" }
                },
                Reviews = new List<ProductReviewResponse>()
                 {
                     new ProductReviewResponse()
                     {
                         Id = 1,
                         UserId = Guid.NewGuid().ToString(),
                         UserName = "Usuario 1",
                         Value = 4,
                         CreatedAt = DateTime.Now,
                         Review = "Review 1"
                     },
                     new ProductReviewResponse()
                     {
                         Id = 1,
                         UserId = Guid.NewGuid().ToString(),
                         UserName = "Usuario 2",
                         Value = 4,
                         CreatedAt = DateTime.Now,
                         Review = "Review 2"
                     }
                 }
            };
        }

        public static IList<ProductResponse> GetProductsResponse(long producCode1, long producCode2)
        {
            return new List<ProductResponse>() { GetProductResponse(producCode1), GetProductResponse(producCode2) };
        }
    }
}
