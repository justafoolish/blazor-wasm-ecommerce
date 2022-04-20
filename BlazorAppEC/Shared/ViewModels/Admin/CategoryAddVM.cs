using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace BlazorAppEC.Shared
{
    public class CategoryAddVM : ICategoryAddVM
    {
        private HttpClient _httpClient;
        public CategoryAddVM() {

        }
        public CategoryAddVM(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public Category Category { get; set; } = new Category();

        public async Task<HttpResponseMessage> CreateCategory()
        {
            return await _httpClient.PostAsJsonAsync<Category>("api/category", Category);
        }
    }
}
