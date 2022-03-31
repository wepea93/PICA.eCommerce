using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Core.Contracts.Services;
using Products.Core.Helpers.Mappers;
using Products.Core.Objects.Dtos;
using Products.Core.Objects.Request;
using Products.Core.Objects.Responses;
using WebApiProducts.Config;

namespace WebApiAuthorizer.Controllers
{
  //  [AuthorizationFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductServices _productService;
        private readonly IMapperHelper _mapperHelper;

        public ProductController(IProductServices productService, ILogger<ProductController> logger, IMapperHelper mapper)
        {
            _productService = productService;
            _logger = logger;
            _mapperHelper = mapper;
        }

        [HttpGet]
        [Route("Catalog")]
        public async Task<ActionResult<ServiceResponse<ProductCatalogResponse>>> GetProductCatalog([FromQuery] ProductCatalogRequest request)
        {
            var businessObject = new ProductSearchDto(request.CategoryId, request.ProviderId, request.Search, 
                request.Page, request.ItemsByPage, request.Sort, request.MinPrice, request.MaxPrice);
            var result =  _productService.GetProductCatalog(businessObject);
            var products = _mapperHelper.MappToProductResponse(result.Products);
            var response = new ProductCatalogResponse(result.ProductsFound, result.Count, products, result.Page, result.Sort, result.TotalProducts, result.TotalPages);
            return Ok(new ServiceResponse<ProductCatalogResponse>("Successful", response));
        }

        [HttpGet]
        [Route("Detail")]
        public async Task<ActionResult<ServiceResponse<ProductResponse>>> GetProduct([FromQuery] ProductRequest request)
        {
            var result = await _productService.GetProduct(request.ProductCode);
            var response = _mapperHelper.MappToProductResponse(result);
            return Ok(new ServiceResponse<ProductResponse>("Successful", response));
        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductResponse>>>> GetProducts([FromQuery] ProductsRequest request)
        {
            var result = _productService.GetProducts(request.ProductsCode);
            var response = _mapperHelper.MappToProductResponse(result);
            return Ok(new ServiceResponse<IList<ProductResponse>>("Successful", response.ToList()));
        }
    }
}
