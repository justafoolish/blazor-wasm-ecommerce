using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("manufacture")]
    public partial class Manufacture
    {
        [Required]
        [Column("title")]
        [StringLength(50)]
        public string Title { get; set; }
        [Column("slug")]
        [StringLength(50)]
        public string Slug { get; set; }
        [Key]
        [Column("manufacture_id")]
        public int ManufactureId { get; set; }
    }
}
