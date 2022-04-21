using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BlazorAppEC.Shared
{
    public class Discount
    {
        public Discount()
        {
        }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Code { get; set; }
        public int DiscountPercent { get; set; }
        public int DiscountId { get; set; }

    }
}
