using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlazorAppEC.Shared
{
    public interface IManufactureAddVM
    {
        public Manufacture Manufacture {get; set;}
        public Task<bool> CreateManufacture();
    }
}
