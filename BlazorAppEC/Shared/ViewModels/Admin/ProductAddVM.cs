using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace BlazorAppEC.Shared
{
    public class ProductAddVM : IProductAddVM
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Manufacture> Manufactures { get; set; } = new List<Manufacture>();
        public List<Product> Products { get; set; } = new List<Product>();
        public Product Product {get; set;} = new Product();

        private HttpClient _httpClient;
        public ProductAddVM(HttpClient httpClient)
        {
            _httpClient = httpClient;
            Product.CategoryId = -1;
            Product.ManufactureId = -1;

        }
        public async Task initCategories() {
            Categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/category");
            Product.CategoryId = Categories.First().CategoryId;

        }
        public async Task initManufactures() {
            Manufactures = await _httpClient.GetFromJsonAsync<List<Manufacture>>("api/manufacture");
            Product.ManufactureId = Manufactures.First().ManufactureId;
        }
        public async Task AddProduct() {
            if(Product.CategoryId != -1 && Product.ManufactureId != -1) {
                await _httpClient.PostAsJsonAsync<Product>("api/product", Product);
            }
        }
    }
}