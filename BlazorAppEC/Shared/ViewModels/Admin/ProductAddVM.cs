using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlazorAppEC.Shared.Http;
using Blazored.LocalStorage;

namespace BlazorAppEC.Shared
{
    public class ProductAddVM : IProductAddVM
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Manufacture> Manufactures { get; set; } = new List<Manufacture>();
        public List<Product> Products { get; set; } = new List<Product>();
        public Product Product {get; set;} = new Product();

        private HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public ProductAddVM(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            Product.CategoryId = -1;
            Product.ManufactureId = -1;

        }
        public async Task initCategories() {
            Categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/category");
            if(Categories.Count > 0) 
                Product.CategoryId = Categories.First().CategoryId;

        }
        public async Task initManufactures() {
            Manufactures = await _httpClient.GetFromJsonAsync<List<Manufacture>>("api/manufacture");
            if(Manufactures.Count > 0)
                Product.ManufactureId = Manufactures.First().ManufactureId;
        }
        public async Task<bool> AddProduct() {
            if(Product.CategoryId != -1 && Product.ManufactureId != -1 && Product != null) {
                string token = await _localStorage.GetItemAsync<string>("jwt_token");
                Response res = await _httpClient.PostAsync<Response>("api/product",Product,token);
                return res.success;
            }

            return false;
        }
    }
}