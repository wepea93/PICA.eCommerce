using Microsoft.AspNetCore.Mvc;
using Products.Core.Contracts.Services;
using Products.Core.Objects.Responses;
using WebApiProducts.Config;

namespace WebApiProducts.Controllers
{
    
    [AuthorizationFilter]
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
        public ActionResult<ServiceResponse<IEnumerable<ProductCategoryResponse>>> GetProductCategories()
        {
            var result =  _productService.GetProductCategories();
            var response = result?.Select(x => new ProductCategoryResponse(x.Id, x.Category)).AsEnumerable();
            return Ok(new ServiceResponse<IEnumerable<ProductCategoryResponse>>("Successful", response));
        }
    }
}
