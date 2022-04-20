using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using BlazorAppEC.Shared;

namespace BlazorAppEC.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        private readonly IUserLoginVM _userLoginVM;
        public CustomAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorageService, IUserLoginVM userLoginVM)
        {
            _httpClient = httpClient;
            _localStorage = localStorageService;
            _userLoginVM = userLoginVM;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            User currentUser = await GetUserByJWTAsync();

            if (currentUser != null && currentUser.Email != null)
            {
                //create claimsPrincipal
                var claimsPrincipal = GetClaimsPrinciple(currentUser);
                return new AuthenticationState(claimsPrincipal);
            }
            else
            {
                await _localStorage.RemoveItemAsync("jwt_token");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        public async Task<User> GetUserByJWTAsync()
        {
            //pulling the token from localStorage
            var jwtToken = await _localStorage.GetItemAsync<string>("jwt_token");
            if (jwtToken == null) return null;

            jwtToken = $@"""{jwtToken}""";
            return await _userLoginVM.GetUserByJWT(jwtToken);
        }
        public async Task MarkUserAsAuthenticated()
        {
            var user = await GetUserByJWTAsync();
            var claimsPrincipal = GetClaimsPrinciple(user);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("jwt_token");


            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
        private ClaimsPrincipal GetClaimsPrinciple(User currentUser)
        {
            //create a claims
            var claimName = new Claim(ClaimTypes.Name, currentUser.Fullname);
            var claimEmail = new Claim(ClaimTypes.Email, currentUser.Email);
            var claimNameIdentifier = new Claim(ClaimTypes.NameIdentifier, currentUser.UserId.ToString());
            var claimRole = new Claim(ClaimTypes.Role, currentUser.Role == null ? "" : currentUser.Role);

            //create claimsIdentity
            var claimsIdentity = new ClaimsIdentity(new[] { claimEmail, claimNameIdentifier, claimName, claimRole }, "serverAuth");
            //create claimsPrincipal
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return claimsPrincipal;
        }
    }
}