using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace BlazorAppEC.Shared
{
    public class CategoryManageVM : ICategoryManageVM
    {
        private HttpClient _httpClient;
        public CategoryManageVM() {

        }
        public CategoryManageVM(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task initCategory()
        {
            Categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/category");
        }
    }
}
