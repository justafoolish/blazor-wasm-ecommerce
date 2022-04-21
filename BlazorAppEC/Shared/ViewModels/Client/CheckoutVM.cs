using System.Net;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorAppEC.Shared.Authentication;
using Blazored.LocalStorage;
using BlazorAppEC.Shared.Http;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using BlazorAppEC.Shared.Services;

namespace BlazorAppEC.Shared
{
    public class CheckoutVM : ICheckoutVM
    {
        public User User { get; set; } = new User();
        private Order Order { get; set; } = new Order();
        private List<Product> Carts { get; set; } = new List<Product>();
        private List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        private ICartService _cartService { get; }
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        private IToastService _toastService;
        public NavigationManager _nav;
        public CheckoutVM() { }
        public CheckoutVM(HttpClient httpClient, ILocalStorageService localStorage, IToastService toastService, NavigationManager nav, ICartService cartService)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _toastService = toastService;
            _nav = nav;
            _cartService = cartService;

        }
        private void setOrderDetails(int orderID)
        {
            foreach (var item in Carts)
            {
                OrderDetails.Add(new OrderDetail()
                {
                    OrderId = orderID,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }
        }
        public async Task Checkout()
        {
            Carts = await _localStorage.GetItemAsync<List<Product>>("cart");
            string token = await _localStorage.GetItemAsync<string>("jwt_token");
            if (token != string.Empty)
            {
                Order.UserId = User.UserId;
                //send request create order. response and order id or entire order object
                var orderResponse = await _httpClient.PostAsync<Response>("api/order", Order, token);
                if (orderResponse.success)
                {
                    // send order detail object if create order success
                    int orderID = Int32.Parse(orderResponse.data.ToString());
                    setOrderDetails(orderID);
                    var res = await _httpClient.PostAsync<Response>("api/order/detail", OrderDetails, token);
                    if (res.success)
                    {
                        _toastService.ShowSuccess("", res.message);
                        await Task.Delay(3500);
                        _cartService.ClearCart();
                        _nav.NavigateTo("/");
                    }
                    else
                    {
                        _toastService.ShowError("", res.message);
                    }
                }
                else
                {
                    _toastService.ShowError("", orderResponse.message);
                }
            }
        }
    }
}