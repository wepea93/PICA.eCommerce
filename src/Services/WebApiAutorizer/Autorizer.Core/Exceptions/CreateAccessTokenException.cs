using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorizer.Core.Exceptions
{
    public class CreateAccessTokenException : Exception
    {
        public CreateAccessTokenException() 
            : base("Error al generar el access token") { }
    }
}
