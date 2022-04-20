using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorAppEC.Shared.Authentication
{
    public partial class AuthenticationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
