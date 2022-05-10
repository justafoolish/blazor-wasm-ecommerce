using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorAppEC.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BlazorAppEC.Shared.Http;

namespace BlazorAppEC.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private BlazorECContext _appContext;

        private readonly ILogger<CategoryController> _logger;
        private readonly Utility _utility;

        public CustomerController(ILogger<CategoryController> logger, BlazorECContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
            _utility = new Utility();
        }

        [HttpGet]
        public async Task<List<User>> GetCustomers()
        {
            List<User> users = await _appContext.Users.Select(u => new User {
                Fullname = u.Fullname,
                Address = u.Address,
                Phone = u.Phone,
                Email = u.Email,
                Orders = u.Orders
            }).ToListAsync();

            foreach (var item in users)
            {
                _appContext.Entry(item).Collection(u => u.Orders).Load();
            }
        
            return users;
        }

    }
}
