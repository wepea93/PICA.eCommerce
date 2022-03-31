using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core.Objects.Responses
{
    public class ValueResponse
    {
        public bool Result { get; set; }

        public ValueResponse(bool result)
        {
            Result = result;
        }
    }
}
