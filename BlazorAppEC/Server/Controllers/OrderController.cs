using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorAppEC.Server.Models;
using Microsoft.EntityFrameworkCore;
using BlazorAppEC.Shared.Http;
using Microsoft.AspNetCore.Authorization;

namespace BlazorAppEC.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private BlazorECContext _appContext;
        private readonly ILogger<OrderController> _logger;
        public OrderController(ILogger<OrderController> logger, BlazorECContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetListOrder()
        {
            var orders = await _appContext.Orders.ToListAsync();
            
            return Ok(new Response() {success = true, data = orders});
        }

        [HttpGet("{orderID}")]
        public Order GetOrder(int orderID)
        {
            Order order = _appContext.Orders.FirstOrDefault(c => c.OrderId == orderID);

            _appContext.Entry(order).Collection("Products").Load();

            return order;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            order.CreateAt = DateTime.Now;
            order.Status = 0;
            _appContext.Orders.Add(order);
            bool isSuccess = await _appContext.SaveChangesAsync() != 0;

            return isSuccess ? Ok(new Response() { success = true, data = order.OrderId }) : BadRequest(new Response() { success = false, message = "Please try again" }); ;
        }

        [HttpPost("detail")]
        public async Task<ActionResult> insertDetailOrder(List<OrderDetail> orders)
        {
            foreach (var item in orders)
            {
                _appContext.OrderDetails.Add(item);
            }
            bool isSuccess = await _appContext.SaveChangesAsync() != 0;
            if(isSuccess) {
                List<int> productIDs = orders.Select(p => p.ProductId).ToList(); //Lay danh sach ma san pham can update
                foreach(var product in _appContext.Products.Where(p => productIDs.Contains(p.ProductId)).ToList()) {
                    product.Quantity -= orders.Where(p => p.ProductId == product.ProductId).Select(p => p.Quantity).FirstOrDefault();
                }

                await _appContext.SaveChangesAsync();
            }

            return isSuccess ? Ok(new Response() { success = true, message = "Order created successfully" }) : BadRequest(new Response() { success = false, message = "Please try again" });

        }
    }
}
