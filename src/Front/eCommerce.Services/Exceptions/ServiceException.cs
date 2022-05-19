using eCommerce.Commons.Objects.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceResponseError Response { get; private set; }

        public int StatusCode { get; set; }

        public ServiceException(ServiceResponseError response,int statusCode) : base(response.Response)
        {
            Response = response;
            StatusCode = statusCode;
        }
        public ServiceException(ServiceResponseError response, int statusCode, Exception? ex) : base(response.Response, ex)
        {
            Response = response;
            StatusCode = statusCode;
        }
    }
}
