using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorAppEC.Shared.Authentication;

namespace BlazorAppEC.Shared
{
    public interface IUserLoginVM
    {
        public AuthenticationRequest User { get; set; }
        public Task Login();
        public Task<AuthenticationResponse> authenticateJWT();
        public Task<User> GetUserByJWT(string jwt_token);
        public Task Logout();
    }
}
