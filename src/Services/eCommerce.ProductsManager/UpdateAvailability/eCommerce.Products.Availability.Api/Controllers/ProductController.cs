using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Products.Availability.Core.Contracts.Services;
using eCommerce.Products.Availability.Core.Objects.Dtos;
using Microsoft.AspNetCore.Mvc;
using WebApiProducts.Config;

namespace eCommerce.Products.Availability.Api.Controllers
{
    [AuthorizationFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }

        /// <summary>
        /// Permite actualizar precio y stock de un producto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> Post([FromBody] UpdateProductRequest request)
        {
            var response = await _productService.UpdateProduct(new ProductDto(request.ProductCode, request.Price, request.Stock));
            return Ok(new ServiceResponse<bool>("Successful", response));
        }
    }
}
