using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
namespace BlazorAppEC.Shared
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        public CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService) {
            _httpClient = httpClient;
            _localStorage = localStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // User user = await _httpClient.GetFromJsonAsync<User>("api/user/session");
            var state = new AuthenticationState(new ClaimsPrincipal());

            // if(user != null && user.Email != null) {
            //     var claim = new Claim(ClaimTypes.Name, user.Email);

            //     var claimsIdentity = new ClaimsIdentity(new[] {claim}, "serverAuth");

            //     var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //     // return new AuthenticationState(claimsPrincipal);
            //     state = new AuthenticationState(claimsPrincipal);
            // }
            // NotifyAuthenticationStateChanged(Task.FromResult(state));

            // return state;
            string token = await _localStorage.GetItemAsync<string>("token");
            if(!string.IsNullOrEmpty(token)) {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, token)
                }, "test authentication type");

                state = new AuthenticationState(new ClaimsPrincipal(identity));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}