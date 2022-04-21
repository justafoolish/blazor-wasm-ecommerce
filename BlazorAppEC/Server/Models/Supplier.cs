using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("supplier")]
    public partial class Supplier
    {
        [Key]
        [Column("supplier_id")]
        public int SupplierId { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }
    }
}
