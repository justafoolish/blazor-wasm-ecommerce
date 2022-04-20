using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorAppEC.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppEC.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManufactureController : ControllerBase
    {
        private BlazorECContext _appContext;

        private readonly ILogger<ManufactureController> _logger;

        public ManufactureController(ILogger<ManufactureController> logger, BlazorECContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }
        
        [HttpGet]
        public async Task<List<Manufacture>> GetListManufacture() {
            return await _appContext.Manufactures.ToListAsync();

        }

    }
}
