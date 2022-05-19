using eCommerce.Customers.Core.Contracts.Services;
using eCommerce.Customers.Core.Helpers.Mappers;
using eCommerce.Customers.Core.Objects.Dtos;
using eCommerce.Customers.Core.Objects.Request;
using eCommerce.Customers.Core.Objects.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCommerce.Customers.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private ICustomerService _customerService;
        private IMapperHelper _mapperHelper;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger, IMapperHelper mapperHelper)
        {
            _customerService = customerService;
            _logger = logger;
            _mapperHelper = mapperHelper;
        }
        [HttpGet]
        [Route("List")]
        [Authorize("read:customer")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<CustomerResponse>>>> GetCustomers([FromQuery] string Id)
        {
            var result = _customerService.GetCustomerById(Id);
            if(result==null)
                return NotFound();
            var nameAuth = _customerService.GetNameAuthentication(result.AutenticationTypeId);
            var response = _mapperHelper.MappToCustomerResponse(result,nameAuth);
            return Ok(new ServiceResponse<CustomerResponse>("Successful", response));
        }

        // POST api/<ValuesController>
        [HttpPost]
        [Route("Create")]
      //  [Authorize("create:customer")]
        public async Task<ActionResult<ServiceResponse<bool>>> Create([FromBody] CustomerRequest request)
        {
            var message = "Success";
            var messageDetail = new { message1 = "", message2 = "" };
            var result = false;
            try
            {
                var businessObject = new CustomerDto(request.Id, request.Identification, request.IdentTypeId, request.FirstName, request.SecondName, request.LastName, request.SecondLastName, request.Email, request.Phone1, request.Phone2, request.UserName, request.Password, request.AutenticationTypeId, request.Status, request.CreatedAt);
                result = await _customerService.Create(businessObject);
                 message = "Success";
                if (!result)
                    message = "Error al crear cliente";
            }
            catch (Exception ex)
            {
                messageDetail = new { message1 = ex.Message.ToString(), message2 = "Create Customer" };
                _logger.LogInformation(message, messageDetail);
                _logger.LogWarning(message, messageDetail);
                _logger.LogError(message, messageDetail);
                return BadRequest();

            }


            return Ok(new ServiceResponse<bool>("Operación exitosa", result));
        }
        
        // POST api/<ValuesController>
        [HttpPost]
        [Route("Update")]
        [Authorize("update:customer")]
        public async Task<ActionResult<ServiceResponse<bool>>> Update([FromBody] CustomerUpRequest request)
        {
            var message = "Success";
            var messageDetail = new { message1 = "", message2 = "" };
            var result = false;
            try
            {
                var businessObject = new CustomerUpdateDto(request.Id, request.Identification, request.IdentTypeId, request.FirstName, request.SecondName, request.LastName, request.SecondLastName, request.Email, request.Phone1, request.Phone2, request.UserName, request.Password, request.AutenticationTypeId, request.Status, request.CreatedAt);
                 result = await _customerService.Update(businessObject);
                message = "Success";
                if (!result)
                    message = "Error al Actualizar cliente";
               
            }
            catch (Exception ex)
            {
                messageDetail = new { message1 = ex.Message.ToString(), message2 = "Update Customer"};
                _logger.LogInformation(message, messageDetail);
                _logger.LogWarning(message, messageDetail);
                _logger.LogError(message, messageDetail);
                return BadRequest();
            }

            return Ok(new ServiceResponse<bool>("Operación exitosa", result));
        }
    }
}
