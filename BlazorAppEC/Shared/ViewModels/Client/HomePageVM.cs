using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAppEC.Shared
{
    public class HomePageVM : IHomePageVM
    {
        public List<Product> LatestProduct { get; set; } = new List<Product>();
        public List<Product> BestSeller { get; set; } = new List<Product>();
        public Product Product { get; set; }

        private HttpClient _httpClient;

        public HomePageVM() {}
        public HomePageVM(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetLatestProduct() {
            return  await _httpClient.GetFromJsonAsync<List<Product>>("api/product/latest");
        }
        public async Task<List<Product>> GetBestSellerProduct() {
            return  await _httpClient.GetFromJsonAsync<List<Product>>("api/product/bestseller");
        }

        public async Task GetProductDetail(string slug) {
            Product = new Product();
            Product = await _httpClient.GetFromJsonAsync<Product>($"api/product/{slug}");
        }
    }
}