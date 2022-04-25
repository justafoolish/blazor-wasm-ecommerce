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
        public ReceivedNote()
        {
            ReceivedNoteDetails = new HashSet<ReceivedNoteDetail>();
        }

        [Key]
        [Column("receive_note_id")]
        public int ReceiveNoteId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("create_at", TypeName = "smalldatetime")]
        public DateTime? CreateAt { get; set; }
        [Column("update_at", TypeName = "smalldatetime")]
        public DateTime? UpdateAt { get; set; }
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        [InverseProperty("ReceivedNotes")]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("ReceivedNotes")]
        public virtual User User { get; set; }
        [InverseProperty(nameof(ReceivedNoteDetail.ReceivedNote))]
        public virtual ICollection<ReceivedNoteDetail> ReceivedNoteDetails { get; set; }
    }
}
