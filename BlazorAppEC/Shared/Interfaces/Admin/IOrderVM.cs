using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorAppEC.Shared.Authentication;

namespace BlazorAppEC.Shared
{
    public interface IOrderVM
    {
        public int itemPerPage { get; set; }
        public int totalPage { get; set; }
        public int currentPage { get; set; }
        public List<Order> Orders { get; set; }
        public Task GetOrders();
        public double GetBillTotal(List<OrderDetail> orderDetails);
    }
}
