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
    public class ManufactureAddVM : IManufactureAddVM
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public ManufactureAddVM(HttpClient httpClient, ILocalStorageService localStorageService) {
            _httpClient = httpClient;
            _localStorage = localStorageService;
        }
        public Manufacture Manufacture { get; set; } = new Manufacture();

        public async Task<bool> CreateManufacture()
        {
            if(Manufacture.Title != String.Empty) {
                string token = await _localStorage.GetItemAsync<string>("jwt_token");
                Response res = await _httpClient.PostAsync<Response>("api/manufacture",Manufacture,token);
                return res.success;
            }
            return false;
        }
    }
}
