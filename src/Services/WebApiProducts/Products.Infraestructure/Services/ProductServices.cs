using Products.Core.Contracts.Repositories;
using Products.Core.Contracts.Services;
using Products.Core.Helpers.Mappers;
using Products.Core.Objects.Dtos;
using Products.Infraestructure.Models.UnitOfWorks;

namespace Products.Infraestructure.Services
{
    public class ProductServices : IProductServices
    {
        private IProductRepository _productRepository;
        private IMapperHelper _mapper;

        public ProductServices(IProductRepository productRepository, IUnitOfWork unitOfWork, IMapperHelper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetProduct(long productCode)
        {
            var product = await _productRepository.GetProductById(productCode);
            var result = product != null ? _mapper.MappToProductDto(product) : null;
            return result;
        }

        public IEnumerable<ProductDto> GetProducts(IEnumerable<long> productsId)
        {
            var products =  _productRepository.GetProductsById(productsId);
            var result = products != null ? _mapper.MappToProductDto(products) : null;
            return result;
        }


        public ProductCatalogDto GetProductCatalog(ProductSearchDto productSearch)
        {
            var productCatalog =  _productRepository.GetProducts(productSearch.CategoryId, productSearch.ProviderId,
                productSearch.Search, productSearch.Page, productSearch.ItemsByPage, productSearch.Sort, productSearch.MinPrice, productSearch.MaxPrice);
            
            var result = productCatalog.Products != null? _mapper.MappToProductDto(productCatalog.Products) : null;
            var catalog = new ProductCatalogDto(result.ToList(), productSearch.Page, productSearch.Sort.ToString(), productCatalog.TotalProducts, productSearch.ItemsByPage);
            return catalog;
        }
    }
}
