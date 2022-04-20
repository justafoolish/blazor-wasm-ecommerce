using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlazorAppEC.Shared
{
    public interface IUserRegisterVM
    {
        public User user {get; set;}
        public Task<bool> Register();
    }
}
