using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("product")]
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Required]
        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Column("slug")]
        [StringLength(255)]
        public string Slug { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("manufacture_id")]
        public int ManufactureId { get; set; }
        [Column("create_at", TypeName = "smalldatetime")]
        public DateTime CreateAt { get; set; }
        [Column("sold")]
        public int Sold { get; set; }
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("image", TypeName = "text")]
        public string Image { get; set; }
        [Column("price")]
        public int Price { get; set; }

        [InverseProperty(nameof(OrderDetail.Product))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
