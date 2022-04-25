using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BlazorAppEC.Shared
{
    public partial class ReceivedNoteDetail
    {
        public int ReceivedNoteId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual ReceivedNote ReceivedNote { get; set; }
    }
}
