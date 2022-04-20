using System;
using System.Collections.Generic;

namespace BlazorAppEC.Shared
{
    public partial class Manufacture
    {
        public Manufacture()
        {
        }
        public int ManufactureId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
    }
}
