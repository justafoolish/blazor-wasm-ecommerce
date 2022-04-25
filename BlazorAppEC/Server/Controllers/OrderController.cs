using System.Net.Mime;
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
    // [Authorize]
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
        public async Task<List<Order>> GetListOrders(int _page = 0, int _limit = 6) {
            var query = await _appContext.Orders.OrderBy(o => o.CreateAt).Skip(_page != 0 ? (_page - 1 ) * _limit : 0).Take(_limit).ToListAsync();
            foreach(var order in query) {
                var user = _appContext.Entry(order).Reference(o => o.User).Query().Select(u => new User {
                    Fullname = u.Fullname
                }).FirstOrDefault();
                order.User = new User() {Fullname = user.Fullname};
                order.OrderDetails = _appContext.Entry(order).Collection(o => o.OrderDetails).Query().Select(o => new OrderDetail {
                    Quantity = o.Quantity,
                    Price = o.Price
                }).ToList();
                
            }
            return query;
        }
        [HttpGet("count")]
        public async Task<int> CountOrders() {
            return await _appContext.Orders.CountAsync();
        }

        [HttpGet("{orderID}")]
        public async Task<List<OrderDetail>> GetOrder(int orderID)
        {
            List<OrderDetail> orders = await _appContext.OrderDetails.Where(c => c.OrderId == orderID).ToListAsync();

            foreach(var item in orders) {
                var orderRef = _appContext.Entry(item).Reference(o => o.Order).Query().FirstOrDefault();
                var productRef = _appContext.Entry(item).Reference(o => o.Product).Query().FirstOrDefault();
                var userRef = _appContext.Entry(item.Order).Reference(o => o.User).Query().FirstOrDefault();

                item.Product = new Product() {
                    Name = productRef.Name,
                    Image = productRef.Image
                };
                item.Order = new Order() {
                    UserId = orderRef.UserId,
                    CreateAt = orderRef.CreateAt,
                    Status = orderRef.Status,
                    User = new User() {
                        Fullname = userRef.Fullname,
                        Address = userRef.Address,
                        Phone = userRef.Phone
                    }
                };
                
            }
            return orders;
        }
        [HttpPatch("confirm")]
        public async Task<ActionResult> ConfirmOrder([FromBody]int order_id) {

            Order Order = await _appContext.Orders.Where(o => o.OrderId == order_id).FirstOrDefaultAsync();
            if(Order != null) {
                Order.Status = 1;
            }

            bool isSuccess = await _appContext.SaveChangesAsync() != 0;

            return isSuccess ? Ok(new Response() {success = true}) : BadRequest(new Response() {success = false});
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
