using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BlazorAppEC.Shared
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ReceivedNote> ReceivedNotes { get; set; }
    }
}
