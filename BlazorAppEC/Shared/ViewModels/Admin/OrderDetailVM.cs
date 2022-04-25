using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorAppEC.Shared.Http;

namespace BlazorAppEC.Shared
{
    public class OrderDetailVM : IOrderDetailVM
    {
        public List<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
        public Order Order { get; set; } = new Order();
        public User User { get; set; } = new User();

        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public OrderDetailVM(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task InitOrder(int order_id)
        {
            string token = await _localStorage.GetItemAsync<string>("jwt_token");
            OrderDetail = await _httpClient.GetAsync<List<OrderDetail>>($"api/order/{order_id}", token);
            Order = OrderDetail.First().Order;
            User = Order.User;
        }

        public async Task<bool> ConfirmOrder(int order_id)
        {
            string token = await _localStorage.GetItemAsync<string>("jwt_token");
            var Response = await _httpClient.PatchAsync<Response>("api/order/confirm",order_id, token);
            if(Response.success) {
                Order.Status = 1;
            }
            return Response.success;
        }
    }
}