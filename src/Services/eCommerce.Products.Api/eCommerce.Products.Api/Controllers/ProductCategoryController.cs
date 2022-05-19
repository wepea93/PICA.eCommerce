using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Core.Contracts.Services;
using WebApiProducts.Config;

namespace WebApiProducts.Controllers
{
    
    //[AuthorizationFilter]
    [Route("api/Product/Categories")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductServices _productService;

        public ProductCategoryController(IProductServices productService)
        {
            _productService = productService;
        }

        /// <summary>
        ///Consulta de categorias de productos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize]
        //  [Authorize(Roles = "Customer")]
        //  [Authorize("read:categories")]
        public ActionResult<ServiceResponse<IEnumerable<ProductCategoryResponse>>> GetProductCategories([FromQuery] ProductCategoryRequest request)
        {
            var result =  _productService.GetProductCategories(request.ProviderRequired);
            var response = result?.Select(x => new ProductCategoryResponse(x.Id, x.Category, x.Image)
            {
                ProductProviders = x.ProductProviders?.Select(y => new ProductProviderResponse(y.Id, y.Provider))
            }).AsEnumerable();
            return Ok(new ServiceResponse<IEnumerable<ProductCategoryResponse>>("Successful", response));
        }
    }
}
