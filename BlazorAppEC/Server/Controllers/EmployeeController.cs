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
    [Authorize]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private BlazorECContext _appContext;

        private readonly ILogger<EmployeeController> _logger;
        private readonly Utility _utility;

        public EmployeeController(ILogger<EmployeeController> logger, BlazorECContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
            _utility = new Utility();
        }

        [HttpGet]
        public async Task<List<User>> GetListEmployee()
        {
            return await _appContext.Users.Where(u => u.Role == "admin").Select(u => new User {
                Fullname = u.Fullname,
                Role = u.Role,
                Email = u.Email,
                Phone = u.Phone,
                Address = u.Address
            }).ToListAsync();
        }

    }
}
