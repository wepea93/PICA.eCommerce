using Products.Core.Config;
using Products.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core.Objects.Dtos
{
    public  class ProductSearchDto
    {
        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
        public string Search { get; set; }
        public int Page { get; set; }
        public int ItemsByPage { get; set; }
        public UtilitiesHelper.ORDERBY Sort { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        public ProductSearchDto(int categoryId, int providerId, string search, int page, int itemsByPage, string sort, decimal minPrice, decimal maxPrice) 
        {
            CategoryId = categoryId;
            ProviderId = providerId;
            Search = !string.IsNullOrEmpty(search) ? search.TrimStart().TrimEnd() : search;
            Page = page <= 0? 1 : page;
            ItemsByPage = CalculateItemsByPage(itemsByPage);
            MinPrice = minPrice;
            MaxPrice = maxPrice;

            switch (sort)
            {
                case "MaxPrice":
                    Sort = UtilitiesHelper.ORDERBY.MaxPrice;
                    break;
                case "Relevance":
                    Sort = UtilitiesHelper.ORDERBY.Relevance;
                    break;
                default:
                    Sort = UtilitiesHelper.ORDERBY.MinPrice;
                    break;
            }

        }

        private int CalculateItemsByPage(int itemsByPage) 
        {
            if (itemsByPage >= Convert.ToInt32(AppConfiguration.Configuration["AppConfiguration:MaxItemsByPage"].ToString()))
                return Convert.ToInt32(AppConfiguration.Configuration["AppConfiguration:MaxItemsByPage"].ToString());

            if (itemsByPage <= 0)
                return Convert.ToInt32(AppConfiguration.Configuration["AppConfiguration:DefaultItemsByPage"].ToString());

            return itemsByPage;
        }
    }
}
