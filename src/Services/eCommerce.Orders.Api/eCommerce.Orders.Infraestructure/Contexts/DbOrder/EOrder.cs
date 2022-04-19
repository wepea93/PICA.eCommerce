using System;
using System.Collections.Generic;

namespace eCommerce.Orders.Infraestructure.Contexts.DbOrder
{
    public partial class EOrder
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DateRequired { get; set; }
        public DateTime? DateShipped { get; set; }
        public int? StatusId { get; set; }
        public string? Comment { get; set; }
        public int? CustomerId { get; set; }

        public virtual EOrderState? Status { get; set; }
    }
}
