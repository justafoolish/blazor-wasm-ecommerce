using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("discount")]
    public partial class Discount
    {
        public Discount()
        {
            Orders = new HashSet<Order>();
        }

        [Column("start_at", TypeName = "smalldatetime")]
        public DateTime StartAt { get; set; }
        [Column("end_at", TypeName = "smalldatetime")]
        public DateTime EndAt { get; set; }
        [Required]
        [Column("code")]
        [StringLength(20)]
        public string Code { get; set; }
        [Column("discount_percent")]
        public int DiscountPercent { get; set; }
        [Key]
        [Column("discount_id")]
        public int DiscountId { get; set; }

        [InverseProperty(nameof(Order.Discount))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
