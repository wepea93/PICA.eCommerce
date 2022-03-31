using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core.Objects.DbTypes
{
    public class ProductProviderEntity
    {
        public int Id { get; set; }
        public string Provider { get; set; } = null!;
        public bool Status { get; set; }
    }
}
