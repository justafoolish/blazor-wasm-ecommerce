using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BlazorAppEC.Shared
{
    public class Order
    {
        public Order()
        {
        }

        public int UserId { get; set; }
        public int? DiscountId { get; set; }
        public int Status { get; set; }
        public DateTime CreateAt { get; set; }
        public int OrderId { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
