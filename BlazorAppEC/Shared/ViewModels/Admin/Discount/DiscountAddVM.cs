using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Blazored.LocalStorage;
using BlazorAppEC.Shared.Http;

namespace BlazorAppEC.Shared
{
    public class DiscountAddVM : IDiscountAddVM
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public DiscountAddVM(HttpClient httpClient, ILocalStorageService localStorageService) {
            _httpClient = httpClient;
            _localStorage = localStorageService;
            Discount.StartAt = DateTime.Now;
            Discount.EndAt = DateTime.Now;
        }
        public Discount Discount { get; set; } = new Discount();

        public async Task<bool> CreateDiscount()
        {
            if(Discount.Code != String.Empty && Discount.StartAt < Discount.EndAt && Discount.DiscountPercent > 0) {
                string token = await _localStorage.GetItemAsync<string>("jwt_token");
                Response res = await _httpClient.PostAsync<Response>("api/discount",Discount,token);
                return res.success;
            }
            return false;
        }
    }
}
