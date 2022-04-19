using Products.Core.Contracts.Repositories;
using Products.Core.Contracts.Services;
using Products.Core.Helpers.Mappers;
using Products.Core.Objects.DbTypes;
using Products.Core.Objects.Dtos;
using Products.Infraestructure.Models.UnitOfWorks;

namespace Products.Infraestructure.Services
{
    public class ProductServices : IProductServices
    {
        private IProductRepository _productRepository;
        private IProductProviderRepository _productProviderRepository;
        private IProductCategoryRepository _productCategoryRepository;
        private IProductReviewRepository _productReviewRepository;
        private IUnitOfWork _unitOfWork;
        private IMapperHelper _mapper;

        public ProductServices(IProductRepository productRepository,
            IProductProviderRepository productProviderRepository,
            IProductCategoryRepository productCategoryRepository,
            IProductReviewRepository productReviewRepository,
            IUnitOfWork unitOfWork, IMapperHelper mapper)
        {
            _productRepository = productRepository;
            _productProviderRepository = productProviderRepository;
            _productReviewRepository = productReviewRepository;
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
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
                productSearch.Search, productSearch.Page, productSearch.ItemsByPage, productSearch.Sort, productSearch.MinPrice, productSearch.MaxPrice, productSearch.Condition);
            
            var result = productCatalog.Products != null? _mapper.MappToProductDto(productCatalog.Products) : null;
            var catalog = new ProductCatalogDto(result.ToList(), productSearch.Page, productSearch.Sort.ToString(), productCatalog.TotalProducts, productSearch.ItemsByPage);
            return catalog;
        }

        public IEnumerable<ProductCategoryDto>? GetProductCategories(bool providerRequired)
        {
            var categories = providerRequired ? _productCategoryRepository.GetProductCategoriesWithProviders() 
                : _productCategoryRepository.GetProductCategories();
            return categories?.Select(x => new ProductCategoryDto(x.Id, x.Category, x.Image) 
            {
                ProductProviders = x.ProductProviderEntities?.Select(y => new ProductProviderDto(y.Id, y.Provider))
            }).AsEnumerable();
        }

        public IEnumerable<ProductProviderDto>? GetProductProviders(int productCategoryId)
        {
            var providers = _productProviderRepository.GetProductProvidersByProductCategoryId(productCategoryId);
            return providers?.Select(x => new ProductProviderDto(x.Id, x.Provider)).AsEnumerable();
        }

        public async Task CreateProductReview(ProductReviewDto productReview)
        {
            var productReviwEntity = new ProductReviewEntity(productReview.ProductId, productReview.UserId, productReview.UserName,
                productReview.Review, productReview.Value);
            _productReviewRepository.CreateProductReviewAsync(productReviwEntity);
            await _unitOfWork.ConfirmAsync();
        }
    }
}
