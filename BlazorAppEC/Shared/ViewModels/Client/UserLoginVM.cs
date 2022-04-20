using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Text;
using BlazorAppEC.Shared.Authentication;
using System.Net.Http.Headers;

namespace BlazorAppEC.Shared
{
    public class UserLoginVM : BlazorAppEC.Shared.IUserLoginVM
    {
        public AuthenticationRequest User { get; set; }
        public AuthenticationResponse _auth {get; set;}
        private HttpClient _httpClient;
        public UserLoginVM()
        {
            User = new AuthenticationRequest();
        }
        public UserLoginVM(HttpClient httpClient)
        {
            _httpClient = httpClient;
            User = new AuthenticationRequest();
        }
        public async Task Login()
        {
            await _httpClient.PostAsJsonAsync<AuthenticationRequest>("api/user/login", User);
        }

        public async Task<AuthenticationResponse> authenticateJWT() {
            var httpMessageResponse = await _httpClient.PostAsJsonAsync("api/user/authjwt", User);

            return await httpMessageResponse.Content.ReadFromJsonAsync<AuthenticationResponse>();
        }

        public async Task Logout()
        {
            await _httpClient.GetAsync("api/user/logout");
        }

        public async Task<User> GetUserByJWT(string jwt_token)
        {
            try
            {
                //preparing the http request
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "api/user/getuserbyjwt")
                {
                    Content = new StringContent(jwt_token)
                    {
                        Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                    }
                };

                //making the http request
                var response = await _httpClient.SendAsync(requestMessage);

                //returning the user if found
                var returnedUser = await response.Content.ReadFromJsonAsync<User>();
                if (returnedUser != null) return await Task.FromResult(returnedUser);
                else return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.GetType());
                return null;
            }  
        }
    }
}
