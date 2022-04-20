using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("order_detail")]
    public partial class OrderDetail
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public int Price { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("OrderDetails")]
        public virtual Product Product { get; set; }
    }
}
