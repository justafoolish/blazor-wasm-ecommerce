using System.Threading.Tasks;
using System.Collections.Generic;
namespace BlazorAppEC.Shared
{
    public interface IProductAddVM
    {
        public List<Category> Categories { get; set; }
        public List<Manufacture> Manufactures { get; set; }
        public Product Product { get; set; }
        public Task initCategories();
        public Task initManufactures();
        public Task<bool> AddProduct();
        public Task GetProductUpdate(string slug);
        public Task<bool> UpdateProduct();
    }
}