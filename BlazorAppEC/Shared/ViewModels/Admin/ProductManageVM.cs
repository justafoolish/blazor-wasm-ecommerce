using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Blazored.LocalStorage;

namespace BlazorAppEC.Shared
{
    public class ProductManageVM : IProductManageVM
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int itemPerPage { get; set; } = 10;
        public int totalPage { get; set; }
        public int currentPage { get; set; } = 1;
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        public ProductManageVM(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task GetProducts() {
            string token = await _localStorage.GetItemAsync<string>("jwt_token");
            Products = await _httpClient.GetAsync<List<Product>>($"api/product?_page={currentPage}&_limit={itemPerPage}", token);
            int totalProduct = await _httpClient.GetAsync<int>("api/order/count",token);
            totalPage = (totalProduct / itemPerPage) + (totalProduct % itemPerPage == 0 ? 0 : 1);
        }

    }
}
