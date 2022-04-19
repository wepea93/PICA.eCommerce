using System;
using System.Collections.Generic;

namespace eCommerce.Orders.Infraestructure.Contexts.DbOrder
{
    public partial class EOrderState
    {
        public EOrderState()
        {
            EOrders = new HashSet<EOrder>();
        }

        public int Id { get; set; }
        public string State { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<EOrder> EOrders { get; set; }
    }
}
