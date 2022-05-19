using eCommerce.Blazor.Demo.Services;
using eCommerce.Commons.Objects.Requests.ShoppingCart;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.ShoppingCart;
using eCommerce.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.Services
{
    public class ShoppingService : BaseHttpClient, IShoppingService
    {
        private const string Controller = "ShoppingCart";
        public ShoppingService(HttpClient http, IConfiguration configuration) : base(http, configuration)
        {

        }

        public async Task<ServiceResponse<bool>> AddShoppingCart(CreateShoppingCartRequest request)
        {
            try
            {
                string uri = GetUri(Controller);
                return await PostAsync<ServiceResponse<bool>>(uri, SetJsonContent(request));
            }
            catch
            {
                throw;
            }
        }

        public async Task<ServiceResponse<bool>> DeleteShoppingCart(DeleteShoppingCartRequest request)
        {
            try
            {
                const string Metodo = "";
                string uri = SetUriQueryParameters(request, Controller, Metodo);
                return await DeleteAsync<ServiceResponse<bool>>(uri);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ServiceResponse<IEnumerable<ShoppingCartResponse>>> GetShoppingCart(GetShoppingCartRequest request)
        {
            try
            {
                const string Metodo = "";
                string uri = SetUriQueryParameters(request, Controller, Metodo);
                return await GetAsync<ServiceResponse<IEnumerable<ShoppingCartResponse>>>(uri);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ServiceResponse<bool>> UpdateShoppingCart(UpdateShoppingCartRequest request)
        {
            try
            {
                string uri = GetUri(Controller);
                return await PutAsync<ServiceResponse<bool>>(uri, SetJsonContent(request));
            }
            catch
            {
                throw;
            }
        }
    }
}
