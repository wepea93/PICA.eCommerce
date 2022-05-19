using eCommerce.Commons.Objects.Requests.ShoppingCart;
using eCommerce.Commons.Objects.Responses;
using eCommerce.Commons.Objects.Responses.ShoppingCart;
using eCommerce.ShoppingCart.Core.Contracts.Services;
using eCommerce.ShoppingCart.Core.Helpers.Mappers;
using eCommerce.ShoppingCart.Core.Objects.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProducts.Config;

namespace WebApiAuthorizer.Controllers
{
    //[AuthorizationFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMapperHelper _mapperHelper;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IMapperHelper mapperHelper)
        {
            _shoppingCartService = shoppingCartService;
            _mapperHelper = mapperHelper;
        }


        /// <summary>
        /// Permite obtener el carrito de compras de un usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize("read:shopppingCart")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ShoppingCartResponse>>>> Get([FromQuery] GetShoppingCartRequest request)
        {
            var result = await _shoppingCartService.GetShoppingCartByUserId(request.CustomerId);
            var response = result?.Select(x => _mapperHelper.MappToShoppingCartResponse(x));
            return Ok(new ServiceResponse<IEnumerable<ShoppingCartResponse>>("Successful", response));
        }

        /// <summary>
        /// Permite crear un carrito de compras para un usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize("create:shopppingCart")]
        public async Task<ActionResult<ServiceResponse<bool>>> Post([FromBody] CreateShoppingCartRequest request)
        {
            var shoppingCartList = request.ShoppingCartItems.Select(x => new ShoppingCartDto(x.CustomerId, x.ProductId, x.Price, x.Quantity));
            var response = await _shoppingCartService.CreateShoppingCart(shoppingCartList);
            return Ok(new ServiceResponse<bool>("Successful", response));
        }

        /// <summary>
        /// Permite actualizar un item de un carrito de compras
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        //[Authorize("update:shopppingCart")]
        public async Task<ActionResult<ServiceResponse<bool>>> Put([FromBody] UpdateShoppingCartRequest request)
        {
            var shoppingCart = new ShoppingCartDto(request.CustomerId, request.ProductId, request.Quantity);
            var response = await _shoppingCartService.UpdateShoppingCart(shoppingCart);
            return Ok(new ServiceResponse<bool>("Successful", response));
        }

        /// <summary>
        /// Permite eliminar el carrito de compras de un usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        //[Authorize("delete:shopppingCart")]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete([FromBody] DeleteShoppingCartRequest request)
        {

            var response = request.ProductId > 0 ? await _shoppingCartService.DeleteShoppingCartItem(new ShoppingCartDto(request.CustomerId, request.ProductId))
                : await _shoppingCartService.DeleteShoppingCartByUser(request.CustomerId);
            return Ok(new ServiceResponse<bool>("Successful", response));
        }
    }
}
