using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace BlazorAppEC.Shared
{
    public class EmployeeManageVM : IEmployeeManageVM
    {
        public List<User> Employees { get; set; } = new List<User>();
        private HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;


        public EmployeeManageVM()
        {

        }
        public EmployeeManageVM(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task initEmployees()
        {
            string token = await _localStorage.GetItemAsync<String>("jwt_token");
            Employees = await _httpClient.GetAsync<List<User>>("api/employee",token);
        }
    }
}
