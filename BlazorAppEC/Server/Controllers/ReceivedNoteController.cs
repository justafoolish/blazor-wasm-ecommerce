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
    [Authorize(Roles = "admin")]
    public class ReceivedNoteController : ControllerBase
    {
        private const bool V = true;
        private BlazorECContext _appContext;

        private readonly ILogger<ReceivedNoteController> _logger;

        public ReceivedNoteController(ILogger<ReceivedNoteController> logger, BlazorECContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
        }

        [HttpGet("supplier")]
        public async Task<List<Supplier>> GetListSupplier()
        {
            return await _appContext.Suppliers.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> CreateReceived(ReceivedNote received) {
            received.CreateAt = DateTime.Now;
            _appContext.ReceivedNotes.Add(received);
            bool isSuccess = await _appContext.SaveChangesAsync() != 0;
            if(isSuccess) {
                List<int> productIDs = received.ReceivedNoteDetails.Select(p => p.ProductId).ToList(); //Lay danh sach ma san pham can update
                foreach(var product in _appContext.Products.Where(p => productIDs.Contains(p.ProductId)).ToList()) {
                    product.Quantity += received.ReceivedNoteDetails.Where(p => p.ProductId == product.ProductId).Select(p => p.Quantity).FirstOrDefault();
                }
                await _appContext.SaveChangesAsync();
            }

            return isSuccess ? Ok(new Response() {success = true, message = "Thêm thành công", data = received}) : BadRequest(new Response() {success = false, message = "Thêm thất bại", data = received});
        }
        [HttpGet]
        public async Task<ActionResult> GetListReceivedNote(int _page = 0, int _limit = 6) {
            var receivedNotes = await _appContext.ReceivedNotes.OrderBy(o => o.CreateAt).Skip(_page != 0 ? (_page - 1 ) * _limit : 0).Take(_limit).ToListAsync();
            
            
            foreach (var item in receivedNotes)
            {
                var userRef = _appContext.Entry(item).Reference(p => p.User).Query().FirstOrDefault();
                var goodsRef = _appContext.Entry(item).Collection(p => p.ReceivedNoteDetails).Query().ToList();
                var supRef = _appContext.Entry(item).Reference(p => p.Supplier).Query().FirstOrDefault();

                item.Supplier = new Supplier() {
                    Name = supRef.Name,
                };
                item.User = new User() {
                    Fullname = userRef.Fullname
                };
                if(goodsRef.Count > 0) {
                    item.ReceivedNoteDetails = goodsRef;
                }

            }

            return Ok(new Response() {success = true, data = receivedNotes});
        }
        [HttpGet("count")]
        public async Task<int> CountGRN() {
            return await _appContext.ReceivedNotes.CountAsync();
        }

    }
}
