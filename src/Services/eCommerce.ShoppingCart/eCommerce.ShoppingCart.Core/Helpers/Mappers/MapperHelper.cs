using eCommerce.Commons.Objects.Responses.Products;
using eCommerce.Commons.Objects.Responses.ShoppingCart;
using eCommerce.ShoppingCart.Core.Objects.Dtos;

namespace eCommerce.ShoppingCart.Core.Helpers.Mappers
{
    public class MapperHelper : IMapperHelper
    {
        public ShoppingCartResponse MappToShoppingCartResponse(ShoppingCartDto dto)
        {
            var product = dto.Product != null ? new ProductResponse()
            {
                Code = dto.Product.Id,
                Name = dto.Product.Name,
                Image = dto.Product.Image,  
            } : new ProductResponse();

            return new ShoppingCartResponse()
            {
                CustomerId = dto.CustomerId,
                Id = dto.Id,
                InitialPrice = dto.InitialPrice,
                Price = dto.Price,
                Product = product,
                SigDiff = GetSigDiff(dto.InitialPrice, dto.Price),
                PercentDiff = CalculatePercentDiff(dto.InitialPrice, dto.Price),
                Quantity = dto.Quantity,
                Available = dto.Product != null ? dto.Product.Available : false
            };
        }

        private static string GetSigDiff(decimal initialPrice, decimal price) 
        {
            return initialPrice > price ? "-" : initialPrice == price ? "=" : "+";
        }

        private static decimal CalculatePercentDiff(decimal initialPrice, decimal price)
        {
            decimal value = 0;
            if (initialPrice == price) return value;

            decimal minValue;
            decimal maxValue;
            if (initialPrice > price)
            {
                minValue = price;
                maxValue = initialPrice;
            }
            else
            {
                minValue = initialPrice;
                maxValue = price;
            }

            decimal result = (minValue / maxValue) * 100;
            var strResult = result > 0 ? result.ToString("#.#") : "0";
            return Convert.ToDecimal(strResult);
        }
    }
}
