using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.Products;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Products.Core.Contracts.Services;
using Products.Core.Helpers.Mappers;
using Products.Core.Objects.Dtos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApiAuthorizer.Controllers;
using WebApiProducts.Controllers;
using Xunit;

namespace WebApiProductsTest
{
    public  class ProductTests
    {
        [Fact]
        public async Task Get_Produt_Successful() 
        {
            var productService = new Mock<IProductServices>();
            var mapper = new Mock<IMapperHelper>(); 
            var productId = 1000;

            var productDto = ProductTestHelper.GetProductDto(productId);
            var productResponse = ProductTestHelper.GetProductResponse(productId);

            productService.Setup(x => x.GetProduct(productId)).ReturnsAsync(productDto);
            mapper.Setup(x => x.MappToProductResponse(productDto)).Returns(productResponse);
            ProductController controlller = new (productService.Object, mapper.Object);

            var request = new ProductRequest() { ProductCode = productId };
            var servResult = await controlller.GetProduct(request);
            var servResponse = servResult.Result as ObjectResult;
            var jsonServResponse = JsonConvert.SerializeObject(servResponse.Value, Formatting.Indented);
            var objectResponse = JsonConvert.DeserializeObject<ServiceResponse<ProductResponse>>(jsonServResponse);

            Assert.Equal(((int)HttpStatusCode.OK), servResponse.StatusCode);
            Assert.NotNull(objectResponse);
            Assert.NotNull(objectResponse);
            Assert.NotEmpty(objectResponse.Message);
            Assert.NotNull(objectResponse.Response);
            Assert.Equal(objectResponse.Response.Code, productId);
            Assert.NotEmpty(objectResponse.Response.Name);
            Assert.NotNull(objectResponse.Response.Category);
            Assert.NotNull(objectResponse.Response.Provider);
            Assert.NotNull(objectResponse.Response.Description);
            Assert.NotNull(objectResponse.Response.Reviews);
            Assert.True(Convert.ToInt32(objectResponse.Response.Score) >= 0);
        }

        [Fact]
        public async Task Get_Produt_List_Successful() 
        {
            var productService = new Mock<IProductServices>();
            var mapper = new Mock<IMapperHelper>();
            var productsId = new List<long> { 1000, 10001 };

            var productsDto = ProductTestHelper.GetProductsDto(1000, 10001);
            var productsResponse = ProductTestHelper.GetProductsResponse(1000, 10001);

            productService.Setup(x => x.GetProducts(productsId)).Returns(productsDto);
            mapper.Setup(x => x.MappToProductResponse(productsDto)).Returns(productsResponse);
            ProductController controlller = new(productService.Object, mapper.Object);

            var request = new ProductsRequest() { ProductsCode = productsId };
            var servResult = await controlller.GetProducts(request);
            var servResponse = servResult.Result as ObjectResult;
            var jsonServResponse = JsonConvert.SerializeObject(servResponse.Value, Formatting.Indented);
            var objectResponse = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<ProductResponse>>>(jsonServResponse);

            Assert.Equal(((int)HttpStatusCode.OK), servResponse.StatusCode);
            Assert.NotNull(objectResponse);
            Assert.NotNull(objectResponse);
            Assert.NotEmpty(objectResponse.Message);
            Assert.NotNull(objectResponse.Response);
            Assert.Contains(objectResponse.Response, x =>x.Code == productsId[0]);
            Assert.Contains(objectResponse.Response, x => x.Code == productsId[1]);
        }

        [Fact]
        public void Get_Produt_Categories_Successful() 
        {
            var productService = new Mock<IProductServices>();
            var productCategoryId1 = 1;
            var productCategoryId2 = 2;
            var categoriesResponse = new List<ProductCategoryDto>()
            {
                new ProductCategoryDto(productCategoryId1, "Product Category 1", "https://localhost/image"),
                new ProductCategoryDto(productCategoryId2, "Product Category 2", "https://localhost/image")
            };

            productService.Setup(x => x.GetProductCategories(false)).Returns(categoriesResponse);
            ProductCategoryController controlller = new(productService.Object);

            var request = new ProductCategoryRequest() { ProviderRequired = false };
            var servResult =  controlller.GetProductCategories(request);
            var servResponse = servResult.Result as ObjectResult;
            var jsonServResponse = JsonConvert.SerializeObject(servResponse.Value, Formatting.Indented);
            var objectResponse = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<ProductCategoryResponse>>>(jsonServResponse);

            Assert.Equal(((int)HttpStatusCode.OK), servResponse.StatusCode);
            Assert.NotNull(objectResponse);
            Assert.NotNull(objectResponse);
            Assert.NotEmpty(objectResponse.Message);
            Assert.NotNull(objectResponse.Response);
            Assert.Contains(objectResponse.Response, x => x.Id == productCategoryId1);
            Assert.Contains(objectResponse.Response, x => x.Id == productCategoryId2);
        }

        [Fact]
        public void Get_Produt_Providers_Successful() 
        {
            var productService = new Mock<IProductServices>();
            var productCategoryId = 1;
            var productProviderId1 = 1;
            var productProviderId2 = 2;
            var providersResponse = new List<ProductProviderDto>()
            {
                new ProductProviderDto(productProviderId1, "Product Provider 1"),
                new ProductProviderDto(productProviderId2, "Product Provider 2")
            };

            productService.Setup(x => x.GetProductProviders(productCategoryId)).Returns(providersResponse);
            ProductProviderController controlller = new (productService.Object);

            var request = new ProductProviderRequest() 
            { 
                ProductCategoryId = productCategoryId
            };
            var servResult = controlller.GetProductProviders(request);
            var servResponse = servResult.Result as ObjectResult;
            var jsonServResponse = JsonConvert.SerializeObject(servResponse.Value, Formatting.Indented);
            var objectResponse = JsonConvert.DeserializeObject<ServiceResponse<IEnumerable<ProductProviderResponse>>>(jsonServResponse);

            Assert.Equal(((int)HttpStatusCode.OK), servResponse.StatusCode);
            Assert.NotNull(objectResponse);
            Assert.NotNull(objectResponse);
            Assert.NotEmpty(objectResponse.Message);
            Assert.NotNull(objectResponse.Response);
        }

        [Fact]
        public async Task Create_Produt_Review_Successful() 
        {
            var productService = new Mock<IProductServices>();
            var productId = 1000;
            var userId = Guid.NewGuid().ToString();
            var userName = "Ususario de prueba";
            var review = "Lorem Ipsum is simply dummy text of the printing and typesetting industry";
            var value = 5;

            var productReviewDto = new ProductReviewDto(productId, userId, userName, review, value);

            productService.Setup(x => x.CreateProductReview(productReviewDto));
            ProductReviewController controlller = new(productService.Object);

            var request = new ProductReviewRequest()
            {
                ProductCode = productId,
                UserId = userId,
                UserName = userName,
                Review = review,
                Value = value
            };
            var servResult = await controlller.CreateProductReview(request);
            var servResponse = servResult.Result as ObjectResult;
            var jsonServResponse = JsonConvert.SerializeObject(servResponse.Value, Formatting.Indented);
            var objectResponse = JsonConvert.DeserializeObject<ServiceResponse<bool>>(jsonServResponse);

            Assert.Equal(((int)HttpStatusCode.OK), servResponse.StatusCode);
            Assert.NotNull(objectResponse);
            Assert.NotNull(objectResponse);
            Assert.NotEmpty(objectResponse.Message);
            Assert.True(objectResponse.Response);

        }
    }
}
