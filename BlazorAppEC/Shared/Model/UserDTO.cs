using System;
using System.Collections.Generic;

#nullable disable

namespace BlazorAppEC.Shared
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
    }
}
