using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
namespace BlazorAppEC.Shared
{
    public class ManufactureManageVM : IManufactureManage
    {
        private HttpClient _httpClient;
        public ManufactureManageVM() {

        }
        public ManufactureManageVM(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public List<Manufacture> Manufactures { get; set; } = new List<Manufacture>();

        public async Task initManufactures()
        {
            Manufactures = await _httpClient.GetFromJsonAsync<List<Manufacture>>("api/manufacture");
        }
    }
}
