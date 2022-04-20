using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace BlazorAppEC.Shared
{
    public class ProductManageVM : IProductManageVM
    {
        public List<Product> Products { get; set; } = new List<Product>();

        private HttpClient _httpClient;
        public ProductManageVM()
        {
        }
        public ProductManageVM(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task GetProducts() {
            Products = await _httpClient.GetFromJsonAsync<List<Product>>("api/product");
        }

    }
}
