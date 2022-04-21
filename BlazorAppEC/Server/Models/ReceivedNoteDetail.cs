using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BlazorAppEC.Server.Models
{
    [Table("received_note_detail")]
    public partial class ReceivedNoteDetail
    {
        [Key]
        [Column("received_note_id")]
        public int ReceivedNoteId { get; set; }
        [Key]
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public int Price { get; set; }
    }
}
