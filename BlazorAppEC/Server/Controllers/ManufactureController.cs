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
    public class ManufactureController : ControllerBase
    {
        private BlazorECContext _appContext;

        private readonly ILogger<ManufactureController> _logger;
        private readonly Utility _utility;
        public ManufactureController(ILogger<ManufactureController> logger, BlazorECContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
            _utility = new Utility();
        }
        
        [HttpGet]
        public async Task<List<Manufacture>> GetListManufacture() {
            return await _appContext.Manufactures.ToListAsync();

        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateCategory(Manufacture manufacture)
        {
            bool isSuccess = false;
            manufacture.Slug = _utility.GenerateSlug(manufacture.Title);
            Manufacture isSlugExist = _appContext.Manufactures.Where(c => c.Slug == manufacture.Slug).FirstOrDefault();
            if (isSlugExist == null)
            {
                _appContext.Manufactures.Add(manufacture);
                isSuccess = await _appContext.SaveChangesAsync() != 0;
            }

            return isSuccess ? Ok(new Response() {success = true}) : BadRequest(new Response() {success = false});
        }

    }
}
