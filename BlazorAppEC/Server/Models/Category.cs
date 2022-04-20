using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("category")]
    public partial class Category
    {
        [Required]
        [Column("title")]
        [StringLength(50)]
        public string Title { get; set; }
        [Column("slug")]
        [StringLength(50)]
        public string Slug { get; set; }
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }
    }
}
