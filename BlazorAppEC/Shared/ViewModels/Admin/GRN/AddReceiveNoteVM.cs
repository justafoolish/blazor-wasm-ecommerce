using System.Linq;
using System.Xml.Linq;
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
    public class AddReceivedNoteVM : IAddReceivedNote
    {
        public User User { get;set; } = new User();
        public List<Product> Products { get;set; } = new List<Product>();
        public List<Supplier> Suppliers { get;set; } = new List<Supplier>();
        public ReceivedNote ReceivedNote { get;set; } = new ReceivedNote();
        public ReceivedNoteDetail ReceivedNoteItem { get; set; } = new ReceivedNoteDetail();

        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _httpClient;
        public AddReceivedNoteVM(HttpClient httpClient, ILocalStorageService localStorage) {
            _httpClient = httpClient;
            _localStorage = localStorage;
            ReceivedNote.ReceivedNoteDetails = new List<ReceivedNoteDetail>();
        }
        public async Task<bool> CreateReceivedNote()
        {
            if(ReceivedNote.SupplierId > 0 && ReceivedNote.UserId > 0) {
                string token = await _localStorage.GetItemAsync<string>("jwt_token");
                var response = await _httpClient.PostAsync<Response>("api/receivednote",ReceivedNote, token);

                return response.success;
            }
            return false;
        }
        public async Task LoadProducts()
        {
            Products = await _httpClient.GetFromJsonAsync<List<Product>>("api/product");
        }

        public async Task LoadSuppliers()
        {
            string token = await _localStorage.GetItemAsync<string>("jwt_token");
            Suppliers = await _httpClient.GetAsync<List<Supplier>>("api/receivednote/supplier",token);
        }

        public void AddReceivedNoteDetail()
        {
            if(ReceivedNoteItem.ProductId > 0) {
                ReceivedNote.ReceivedNoteDetails.Add(new ReceivedNoteDetail() {
                    ProductId = ReceivedNoteItem.ProductId,
                    Quantity = ReceivedNoteItem.Quantity,
                    Price = ReceivedNoteItem.Price
                });
            }
        }

        public void DeleteReceiveNoteItem(int ProductID)
        {
            ReceivedNote.ReceivedNoteDetails.Remove(ReceivedNote.ReceivedNoteDetails.Where(p => p.ProductId == ProductID).FirstOrDefault());
        }
    }
}
