using System;
using System.Collections.Generic;

namespace BlazorAppEC.Shared
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int ManufactureId { get; set; }
        public int Sold { get; set; }
        public int Price { get; set; }
        public string Slug {get; set;}
        public string Image { get; set; }
    }
}
