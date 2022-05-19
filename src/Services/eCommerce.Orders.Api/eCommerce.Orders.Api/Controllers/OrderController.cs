using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Orders.Core.Contracts;
using eCommerce.Orders.Core.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using eCommerce.Orders.Core.Objects.Responses;
using eCommerce.Orders.Core.Objects.Request;
using eCommerce.Orders.Core.Objects.Dtos;
using eCommerce.Orders.Core.Helpers.Mappers;

namespace eCommerce.Orders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private IOrderService _orderService;
        private IMapperHelper _mapperHelper;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger,IMapperHelper mapperHelper)
        {
            _orderService = orderService;
            _logger = logger;
            _mapperHelper = mapperHelper;
        }
        [HttpPost]
        [Route("Create")]
        [Authorize("create:order")]
        public async Task<ActionResult<ServiceResponse<bool>>> Create([FromBody] OrderRequest request)
        {
            var detailObject = new List<OrderDetailDto>();
            foreach (var item in request.OrderDetail)
            {
                detailObject.Add(new OrderDetailDto(item.ProductID, item.QuantityOrdered, item.PriceEach, item.OrderLine));
            }
            var businessObject = new OrderDto(request.OrderID, request.OrderDate, request.DateRequiered,request.Comment, request.Customer,detailObject);

            var result = await _orderService.Create(businessObject);
            var message = "Success";
            if (!result)
                message = "Erro al crear orden";

             var messageDetail = new { message1 = "msg1", message2 = "msg2" };

            _logger.LogInformation(message, messageDetail);
            _logger.LogWarning(message, messageDetail);
            _logger.LogError(message, messageDetail);
            return Ok(new ServiceResponse<bool>("Operación exitosa", result));
        }

        /// <summary>
        /// Consulta de ordenes por Cliente o ID orden
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        [Authorize("read:order")]
        public async Task<ActionResult<ServiceResponse<OrderResponse>>> GetOrder([FromQuery] OrderSearchRequest request)
        {
            var result = await _orderService.GetOrderByIdOrCustomer(request.OrderID,request.Customer);
            var response = _mapperHelper.MappToOrderResponse(result);
            return Ok(new ServiceResponse<IList<OrderResponse>>("Successful", response));
        }

        /// <summary>
        /// Consulta de detalle de orden por ID
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Detail")]
        [Authorize("read:order")]
        public async Task<ActionResult<ServiceResponse<OrderDetailResponse>>> GetDetailOrder([FromQuery] OrderSearchRequest request)
        {
            var resultDetail = await _orderService.GetOrderDetailById(request.OrderID);
            var response = _mapperHelper.MappToOrderDetailResponse(resultDetail);
            return Ok(new ServiceResponse<IList<OrderDetailResponse>>("Successful", response));
        }

    }
}
