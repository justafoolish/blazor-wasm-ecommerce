using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Blazored.LocalStorage;

namespace BlazorAppEC.Shared
{
    public class DiscountManageVM : IDiscountManageVM
    {
        private readonly ILocalStorageService _localStorage;
        private HttpClient _httpClient;
        
        public DiscountManageVM() {

        }
        public DiscountManageVM(HttpClient httpClient, ILocalStorageService localStorageService) {
            _httpClient = httpClient;
            _localStorage = localStorageService;
        }

        public List<Discount> Discounts { get; set; } = new List<Discount>();

        public async Task initDiscounts()
        {
            string token = await _localStorage.GetItemAsync<string>("jwt_token");
            if(token != string.Empty) 
                Discounts = await _httpClient.GetAsync<List<Discount>>("api/discount",token);
        }
    }
}
