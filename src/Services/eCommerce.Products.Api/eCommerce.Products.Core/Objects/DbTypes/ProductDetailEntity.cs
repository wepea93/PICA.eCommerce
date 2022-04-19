﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Core.Objects.DbTypes
{
    public  class ProductDetailEntity
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
    }
}
