using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("user")]
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            ReceivedNotes = new HashSet<ReceivedNote>();
        }

        [Required]
        [Column("fullname")]
        [StringLength(100)]
        public string Fullname { get; set; }
        [Required]
        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [Column("password")]
        [StringLength(255)]
        public string Password { get; set; }
        [Required]
        [Column("address")]
        [StringLength(255)]
        public string Address { get; set; }
        [Required]
        [Column("phone")]
        [StringLength(25)]
        public string Phone { get; set; }
        [Column("role")]
        [StringLength(50)]
        public string Role { get; set; }
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [InverseProperty(nameof(Order.User))]
        public virtual ICollection<Order> Orders { get; set; }
        [InverseProperty(nameof(ReceivedNote.User))]
        public virtual ICollection<ReceivedNote> ReceivedNotes { get; set; }
    }
}
