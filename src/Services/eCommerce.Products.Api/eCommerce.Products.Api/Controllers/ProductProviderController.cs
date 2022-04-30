using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.Products;
using Microsoft.AspNetCore.Mvc;
using Products.Core.Contracts.Services;
using WebApiProducts.Config;

namespace WebApiProducts.Controllers
{
    [AuthorizationFilter]
    [Route("api/Product/Providers")]
    [ApiController]
    public class ProductProviderController : ControllerBase
    {
        private readonly IProductServices _productService;

        public ProductProviderController(IProductServices productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Consulta lista de proveedores de productos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductProviderResponse>> GetProductProviders([FromQuery] ProductProviderRequest request)
        {
            var result = _productService.GetProductProviders(request.ProductCategoryId);
            var response = result?.Select(x => new ProductProviderResponse(x.Id, x.Provider)).AsEnumerable();
            return Ok(new ServiceResponse<IEnumerable<ProductProviderResponse>>("Successful", response));
        }
    }
}
