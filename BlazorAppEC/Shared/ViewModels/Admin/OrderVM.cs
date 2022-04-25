using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Linq;

namespace BlazorAppEC.Shared {
    public class OrderVM : IOrderVM
    {
        public int itemPerPage { get; set; } = 10;
        public int totalPage { get; set; }
        public int currentPage { get; set; } = 1;
        public List<Order> Orders { get;set; } = new List<Order>();
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;

        public OrderVM(HttpClient httpClient, ILocalStorageService localStorage) {
            _httpClient = httpClient;
            _localStorage = localStorage;

        }
        public async Task GetOrders()
        {
            string token = await _localStorage.GetItemAsync<string>("jwt_token");
            Orders = await _httpClient.GetAsync<List<Order>>($"api/order?_page={currentPage}&_limit={itemPerPage}", token);
            int totalProduct = await _httpClient.GetAsync<int>("api/order/count",token);
            totalPage = (totalProduct / itemPerPage) + (totalProduct % itemPerPage == 0 ? 0 : 1);
        }

        public double GetBillTotal(List<OrderDetail> orderItem)
        {
            return orderItem.Select(o => o.Price * o.Quantity).Sum();
        }
    }
}