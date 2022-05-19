using eCommerce.Commons.Objects.Requests.Products;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Core.Contracts.Services;
using Products.Core.Helpers.Mappers;
using Products.Core.Objects.Dtos;

namespace WebApiAuthorizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;
        private readonly IMapperHelper _mapperHelper;

        public ProductController(IProductServices productService, IMapperHelper mapper)
        {
            _productService = productService;
            _mapperHelper = mapper;
        }

        /// <summary>
        /// Consulta de catálogo de productos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Catalog")]
        //[Authorize]
        //   [Authorize("read:products")]
        public ActionResult<ServiceResponse<ProductCatalogResponse>> GetProductCatalog([FromQuery] ProductCatalogRequest request)
        {
            var businessObject = new ProductSearchDto(request.CategoryId, request.ProviderId, request.Search, 
                request.Page, request.ItemsByPage, request.Sort, request.MinPrice, request.MaxPrice, request.Condition);
            var result =  _productService.GetProductCatalog(businessObject);
            var products = _mapperHelper.MappToProductResponse(result.Products);
            var response = new ProductCatalogResponse(result.ProductsFound, result.Count, products, result.Page, result.Sort, result.TotalProducts, result.TotalPages);
            return Ok(new ServiceResponse<ProductCatalogResponse>("Successful", response));
        }

        /// <summary>
        /// Consulta de un producto por su Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Detail")]
        //[Authorize]
        //   [Authorize("read:products")]
        public async Task<ActionResult<ServiceResponse<ProductResponse>>> GetProduct([FromQuery] ProductRequest request)
        {
            var result = await _productService.GetProduct(request.ProductCode);
            var response = _mapperHelper.MappToProductResponse(result);
            return Ok(new ServiceResponse<ProductResponse>("Successful", response));
        }

        /// <summary>
        /// Consulta una lista de productos por sus Ids
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        //[Authorize]
        //   [Authorize("read:products")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductResponse>>>> GetProducts([FromQuery] ProductsRequest request)
        {
            var result = _productService.GetProducts(request.ProductsCode);
            var response = _mapperHelper.MappToProductResponse(result);
            return Ok(new ServiceResponse<IList<ProductResponse>>("Successful", response.ToList()));
        }
    }
}
