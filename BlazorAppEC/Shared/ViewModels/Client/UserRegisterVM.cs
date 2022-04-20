using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BlazorAppEC.Shared.Authentication;

namespace BlazorAppEC.Shared
{
    public class UserRegisterVM : IUserRegisterVM
    {
        public User user { get; set; }
        private HttpClient _httpClient;
        public UserRegisterVM(){
            user = new User();
        }
        public UserRegisterVM(HttpClient httpClient)
        {
            _httpClient = httpClient;
            user = new User();
        }
        public async Task<bool> Register ()
        {
            var res = await _httpClient.PostAsJsonAsync<User>("api/user/register", user);

            return res.IsSuccessStatusCode;
        }
    }
}
