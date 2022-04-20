using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlazorAppEC.Shared
{
    public interface ICategoryAddVM
    {
        public Category Category {get; set;}
        public Task<HttpResponseMessage> CreateCategory();
    }
}
