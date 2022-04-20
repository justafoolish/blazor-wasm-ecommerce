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
    public class CategoryController : ControllerBase
    {
        private BlazorECContext _appContext;

        private readonly ILogger<CategoryController> _logger;
        private readonly Utility _utility;

        public CategoryController(ILogger<CategoryController> logger, BlazorECContext appContext)
        {
            _logger = logger;
            _appContext = appContext;
            _utility = new Utility();
        }

        [HttpGet]
        public async Task<List<Category>> GetListCategory()
        {
            return await _appContext.Categories.ToListAsync();

        }

        [HttpGet("{categoryID}")]
        public Category GetCategory(int categoryID)
        {
            Category category = _appContext.Categories.FirstOrDefault(c => c.CategoryId == categoryID);

            _appContext.Entry(category).Collection("Products").Load();

            return category;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            category.Slug = _utility.GenerateSlug(category.Title);
            Category isSlugExist = _appContext.Categories.Where(c => c.Slug == category.Slug).FirstOrDefault();
            if (isSlugExist == null)
            {
                _appContext.Categories.Add(category);
                await _appContext.SaveChangesAsync();
            }

            return Ok();
        }

    }
}
