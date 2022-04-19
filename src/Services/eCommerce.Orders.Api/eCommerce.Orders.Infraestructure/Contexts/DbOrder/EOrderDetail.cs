using System;
using System.Collections.Generic;

namespace eCommerce.Orders.Infraestructure.Contexts.DbOrder
{
    public partial class EOrderDetail
    {
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? QuantityOrdered { get; set; }
        public decimal? PriceEach { get; set; }
        public int? OrderLine { get; set; }

        public virtual EOrder? Order { get; set; }
    }
}
