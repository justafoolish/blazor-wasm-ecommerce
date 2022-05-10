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
    public class CategoryAddVM : ICategoryAddVM
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public CategoryAddVM(HttpClient httpClient, ILocalStorageService localStorageService) {
            _httpClient = httpClient;
            _localStorage = localStorageService;
        }
        public Category Category { get; set; } = new Category();

        public async Task<bool> CreateCategory()
        {
            if(Category.Title != String.Empty) {
                string token = await _localStorage.GetItemAsync<string>("jwt_token");
                Response res = await _httpClient.PostAsync<Response>("api/category",Category,token);
                return res.success;
            }
            return false;
        }
    }
}
