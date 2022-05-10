using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Net.Http.Json;
using System.Linq;
using BlazorAppEC.Shared.Http;

namespace BlazorAppEC.Shared {
    public class ReceivedNoteVM : IReceivedNoteVM
    {
        public int itemPerPage { get; set; } = 10;
        public int totalPage { get; set; }
        public int currentPage { get; set; } = 1;
        public List<ReceivedNote> ReceivedNotes { get;set; } = new List<ReceivedNote>();
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;

        public ReceivedNoteVM(HttpClient httpClient, ILocalStorageService localStorage) {
            _httpClient = httpClient;
            _localStorage = localStorage;

        }
        public async Task GetGoodReceivedNotes()
        {
            string token = await _localStorage.GetItemAsync<string>("jwt_token");
            var response = await _httpClient.GetAsync<Response>($"api/receivednote?_page={currentPage}&_limit={itemPerPage}", token);
            if(response.success) {
                ReceivedNotes = Utility.jsonConvert<List<ReceivedNote>>(response.data.ToString());
                int totalResult = await _httpClient.GetAsync<int>("api/receivednote/count",token);
                totalPage = (totalResult / itemPerPage) + (totalResult % itemPerPage == 0 ? 0 : 1);
            }
        }

        public double GetBillTotal(List<ReceivedNoteDetail> receivedItems)
        {
            return receivedItems.Select(o => o.Price * o.Quantity).Sum();

        }
    }
}