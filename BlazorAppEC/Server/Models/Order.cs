using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("order")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Column("user_id")]
        public int UserId { get; set; }
        [Column("discount_id")]
        public int? DiscountId { get; set; }
        [Column("status")]
        public int? Status { get; set; }
        [Column("create_at", TypeName = "smalldatetime")]
        public DateTime? CreateAt { get; set; }
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [ForeignKey(nameof(DiscountId))]
        [InverseProperty("Orders")]
        public virtual Discount Discount { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("Orders")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
