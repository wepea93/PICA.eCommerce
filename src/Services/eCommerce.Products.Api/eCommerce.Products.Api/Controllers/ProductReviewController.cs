using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using Microsoft.AspNetCore.Mvc;
using Products.Core.Contracts.Services;
using Products.Core.Objects.Dtos;
using WebApiProducts.Config;

namespace WebApiProducts.Controllers
{
    [AuthorizationFilter]
    [Route("api/Product/Review")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductServices _productService;

        public ProductReviewController(IProductServices productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Crea un review para un producto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateProductReview(ProductReviewRequest request)
        {
            var productReview = new ProductReviewDto(request.ProductCode, request.UserId, request.UserName, request.Review, request.Value);
            await _productService.CreateProductReview(productReview);
            return Ok(new ServiceResponse<bool>("Successful", true));
        }
    }
}
