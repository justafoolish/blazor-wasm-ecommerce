using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using BlazorAppEC.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BlazorAppEC.Shared.Http;

namespace BlazorAppEC.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly BlazorECContext _appContext;
        private readonly Utility _utility;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, BlazorECContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
            _utility = new Utility();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            Console.Write(product.Description);
            product.Slug = _utility.GenerateSlug(product.Name);
            product.Sold = 0;
            product.CreateAt = DateTime.Now;
            _appContext.Products.Add(product);
            bool isSuccess = await _appContext.SaveChangesAsync() != 0;
            return isSuccess ? Ok(new Response() { success = true, message = "Tạo sản phẩm thành công" }) : BadRequest(new Response() { success = false, message = "Thất bại. Vui lòng thử lại" });
        }

        [HttpGet]
        public async Task<List<Product>> GetListProduct(int _category = -1, int _manufacture = -1, int _page = -1, int _limit = -1)
        {
            var query = _appContext.Products.Where(p => true);
            if (_category != -1 && _category > 0)
            {
                query = query.Where(p => p.CategoryId == _category);
            }
            if (_manufacture != -1 && _manufacture > 0)
            {
                query = query.Where(p => p.ManufactureId == _manufacture);
            }
            if (_page > 0 && _limit > 0)
            {
                query = query
                    .OrderBy(p => p.Name)
                    .Skip((_page - 1) * _limit)
                    .Take(_limit);
            }
            return await query.ToListAsync();
        }
        [HttpGet("latest")]
        public async Task<List<Product>> GetLatestProduct()
        {
            return await _appContext.Products
                .OrderByDescending(p => p.CreateAt)
                .Take(4)
                .ToListAsync();
        }
        [HttpGet("bestseller")]
        public async Task<List<Product>> GetBestSellerProduct()
        {
            return await _appContext.Products
                .OrderByDescending(p => p.Sold)
                .Take(4)
                .ToListAsync();
        }
        [HttpGet("{slug}")]
        public async Task<Product> GetProductAsync(string slug)
        {
            return await _appContext.Products.Where(p => p.Slug == slug).FirstOrDefaultAsync();
        }

        [HttpGet("count")]
        public async Task<int> CountProducts(int _category = -1, int _manufacture = -1)
        {
            var record = _appContext.Products.Where(p => true);

            if (_category != -1 && _category > 0)
            {
                record = record.Where(p => p.CategoryId == _category);
            }
            if (_manufacture != -1 && _manufacture > 0)
            {
                record = record.Where(p => p.ManufactureId == _manufacture);
            }

            return await record.CountAsync();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateProduct([FromBody] Product _prod)
        {
            Product product = await _appContext.Products.Where(p => p.ProductId == _prod.ProductId).FirstOrDefaultAsync();
            if (product != null)
            {
                if (product.Name != _prod.Name) {
                    product.Name = _prod.Name;
                    product.Slug = _utility.GenerateSlug(_prod.Name);
                }
                if (product.Image != _prod.Image)
                    product.Image = _prod.Image;
                if (product.Price != _prod.Price)
                    product.Price = _prod.Price;
                if (product.Quantity != _prod.Quantity)
                    product.Quantity = _prod.Quantity;
                if (product.ManufactureId != _prod.ManufactureId)
                    product.ManufactureId = _prod.ManufactureId;
                if (product.CategoryId != _prod.CategoryId)
                    product.CategoryId = _prod.CategoryId;
                if (product.Description != _prod.Description) {
                    product.Description = _prod.Description;
                    Console.Write(_prod.Description);
                }
            }
            bool isSuccess = await _appContext.SaveChangesAsync() != 0;

            return isSuccess ? Ok(new Response() { success = true }) : BadRequest(new Response() { success = false });

        }

        [HttpGet("test")]
        public async Task<ActionResult> test()
        {
            List<Product> prods = await _appContext.Products.Take(5).ToListAsync();

            return Ok(new Response() { success = true, data = prods });
        }
    }
}