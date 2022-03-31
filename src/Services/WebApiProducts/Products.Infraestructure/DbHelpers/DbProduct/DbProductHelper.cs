﻿using Products.Core.Objects.DbTypes;
using Products.Core.Utilities;
using Products.Infraestructure.Contexts.DbProduct;
using System.Linq.Expressions;

namespace Products.Infraestructure.DbHelpers
{
    public class DbProductHelper : IDbProductHelper
    {
        public Expression<Func<Product, bool>> FiltersExpression(string search, int productCategoryId, int providerId, decimal minPrice, decimal maxPrice)
        {
            Expression<Func<Product, bool>> filters =
                x => !x.Name.Equals("") && x.ProductCategoyId == productCategoryId && x.Status == true;

            if (!string.IsNullOrEmpty(search))
            {
   
                Expression<Func<Product, bool>> filterValues = (x => (x.Name.Contains(search)
                                                                        //|| x.ProductDetails.Any(x=>x.Description.Contains(search))
                                                                        //|| x.ProductSpecifications.Any(x=>x.Description.Contains(search))
                                                                        || x.ProductProvider.Provider.Contains(search)));

                filters = CombineWithAnd(filters, filterValues);
            }
            if (providerId > 0)
            {
                Expression<Func<Product, bool>> filterProvider = x => x.ProductProviderId == providerId;
                filters = CombineWithAnd(filters, filterProvider);
            }

            if (minPrice > 0)
            {
                Expression<Func<Product, bool>> filterMinPrice = x => x.Price >= minPrice;
                filters = CombineWithAnd(filters, filterMinPrice);
            }

            if (maxPrice > 0)
            {
                Expression<Func<Product, bool>> filterMaxPrice = x => x.Price <= maxPrice;
                filters = CombineWithAnd(filters, filterMaxPrice);
            }

            return filters;
        }

        public Func<IQueryable<Product>, IOrderedQueryable<Product>> OrderBy(UtilitiesHelper.ORDERBY orderBy)
        {
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderb = x => x.OrderBy(y => y.Id);
           
            switch (orderBy)
            {
                case UtilitiesHelper.ORDERBY.MinPrice:
                    orderb = x => x.OrderBy(y => y.Price);
                    break;
                case UtilitiesHelper.ORDERBY.MaxPrice:
                    orderb = x => x.OrderByDescending(y => y.Price);
                    break;
                case UtilitiesHelper.ORDERBY.Relevance:
                    orderb = x => x.OrderBy(y => y.ProductReviews.Count);
                    break;
            }
            return orderb;
        }

        public ProductEntity ConvertToProductEntity(Product bdProdcut)
        {
            return new ProductEntity
            {
                Id = bdProdcut.Id,
                Name = bdProdcut.Name,
                Image = bdProdcut.Image,
                CreatedAt = bdProdcut.CreatedAt,
                Price = bdProdcut.Price,
                Status = bdProdcut.Status,
                ProductCategoy = new ProductCategoryEntity()
                {
                    Id = bdProdcut.ProductCategoyId,
                    Category = bdProdcut.ProductCategoy.Category,
                    Status = bdProdcut.ProductCategoy.Status
                },
                ProductProvider = new ProductProviderEntity()
                {
                    Id = bdProdcut.ProductProviderId,
                    Provider = bdProdcut.ProductProvider.Provider,
                    Status = bdProdcut.ProductProvider.Status
                },
                ProductDetails = bdProdcut.ProductDetails.Select(x => new ProductDetailEntity()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Status = x.Status
                }).ToList(),
                ProductSpecifications = bdProdcut.ProductSpecifications.Select(y => new ProductSpecificationEntity()
                {
                    Id = y.Id,
                    Description = y.Description,
                    Value = y.Value,
                    Status = y.Status
                }).ToList(),
                ProductReviews = bdProdcut.ProductReviews.Select(y => new ProductReviewEntity()
                {
                    Id = y.Id,
                    UserId = y.UserId,
                    UserName = y.UserName,
                    Value = y.Value,
                    CreatedAt = y.CreatedAt
                }).ToList(),
            };
        }

        public IEnumerable<ProductEntity> ConvertToProductEntity(IEnumerable<Product> bdProdcuts)
        {
            if (bdProdcuts == null) return null;
            return bdProdcuts.Select(x => ConvertToProductEntity(x));
        }

        private Expression<Func<Product, bool>> CombineWithAnd(Expression<Func<Product, bool>> first, Expression<Func<Product, bool>> second)
        {
            var paramExpr = Expression.Parameter(typeof(Product));
            var exprBody = Expression.And(first.Body, second.Body);
            exprBody = (BinaryExpression)new ReplaceParameterHelper(paramExpr).Visit(exprBody);
            return Expression.Lambda<Func<Product, bool>>(exprBody, paramExpr);
        }
    }
}
