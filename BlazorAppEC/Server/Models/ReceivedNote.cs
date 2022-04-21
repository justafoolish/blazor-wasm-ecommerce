using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("received_note")]
    public partial class ReceivedNote
    {
        [Key]
        [Column("receive_note_id")]
        public int ReceiveNoteId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("create_at", TypeName = "smalldatetime")]
        public DateTime CreateAt { get; set; }
        [Column("update_at", TypeName = "smalldatetime")]
        public DateTime? UpdateAt { get; set; }
    }
}
