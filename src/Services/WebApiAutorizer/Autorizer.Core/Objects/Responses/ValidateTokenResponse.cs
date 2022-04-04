using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorizer.Core.Objects.Responses
{
    public class ValidateTokenResponse
    {
        public bool IsValid { get; set; }

        public ValidateTokenResponse(bool isValid) 
        {
            IsValid = isValid;
        }
    }
}
