using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorAppEC.Shared.Authentication;

namespace BlazorAppEC.Shared
{
    public interface IProductManageVM
    {
        public int itemPerPage { get; set; }
        public int totalPage { get; set; }
        public int currentPage { get; set; }
        public List<Product> Products { get; set; }
        public Task GetProducts();
    }
}
