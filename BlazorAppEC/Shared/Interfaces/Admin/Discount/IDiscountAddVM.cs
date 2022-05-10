using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlazorAppEC.Shared
{
    public interface IDiscountAddVM
    {
        public Discount Discount {get; set;}
        public Task<bool> CreateDiscount();
    }
}
