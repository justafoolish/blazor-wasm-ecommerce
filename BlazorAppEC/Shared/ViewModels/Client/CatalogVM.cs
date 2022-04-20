using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Blazored.LocalStorage;

namespace BlazorAppEC.Shared
{
    public class CatalogVM : ICatalogVM
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Manufacture> Manufactures { get; set; } = new List<Manufacture>();
        public List<Product> Products { get; set; } = new List<Product>();
        public int selectedCategory { get; set; } = -1;
        public int selectedManufacture { get; set; } = -1;
        public int itemPerPage { get; set; } = 6;
        public int totalPage { get; set; }
        public int currentPage { get; set; } = 1;
        private static HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        public CatalogVM(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task GetProducts()
        {
            Products = await _httpClient.GetFromJsonAsync<List<Product>>($"api/product?_category={selectedCategory}&_manufacture={selectedManufacture}&_page={currentPage}&_limit={itemPerPage}");
            int totalProduct = await _httpClient.GetFromJsonAsync<int>($"api/product/count?_category={selectedCategory}&_manufacture={selectedManufacture}");
            totalPage = totalProduct / itemPerPage + 1;
        }

        public async Task initCategories()
        {
            Categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/category");
        }

        public async Task initManufactures()
        {
            Manufactures = await _httpClient.GetFromJsonAsync<List<Manufacture>>("api/manufacture");
        }

        public void resetCat()
        {
            selectedCategory = -1;
        }

        public void resetMan()
        {
            selectedManufacture = -1;
        }
    }
}