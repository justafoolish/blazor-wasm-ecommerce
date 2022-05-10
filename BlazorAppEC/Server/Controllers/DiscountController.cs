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
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
        private BlazorECContext _appContext;

        private readonly ILogger<DiscountController> _logger;

        public DiscountController(ILogger<DiscountController> logger, BlazorECContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }

        [HttpGet]
        public async Task<List<Discount>> GetListDiscount()
        {
            return await _appContext.Discounts.ToListAsync();
        }

        [HttpGet("{discountCode}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetCategory(string discountCode)
        {
            Discount discount = await _appContext.Discounts.Where(d => d.Code == discountCode).FirstOrDefaultAsync();
            
            DateTime now = DateTime.Now;

            if(discount != null && now >= discount.StartAt && now <= discount.EndAt)
            {
                return Ok(new Response() {success = true, data = discount});
            }
            return BadRequest(new Response() {success = false, message = "Discount Code outdated"});
        }

        [HttpPost]
        public async Task<ActionResult> CreateDiscount(Discount discount)
        {
            bool isSuccess = false;
            Discount isCodeExist = _appContext.Discounts.Where(d => d.Code == discount.Code).FirstOrDefault();
            if (isCodeExist == null)
            {
                _appContext.Discounts.Add(discount);
                isSuccess = await _appContext.SaveChangesAsync() != 0;
            }

            return isSuccess ? Ok(new Response() {success = true}) : BadRequest(new Response() {success = false});
        }

    }
}
