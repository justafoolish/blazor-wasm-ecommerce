using System.Threading.Tasks;
using System.Collections.Generic;
namespace BlazorAppEC.Shared
{
    public interface ICatalogVM
    {
        public int itemPerPage { get; set; }
        public int totalPage { get; set; }
        public int currentPage { get; set; }
        public int selectedCategory { get; set; }
        public int selectedManufacture { get; set; }
        public List<Category> Categories { get; set; }
        public List<Manufacture> Manufactures { get; set; }
        public List<Product> Products { get; set; }
        public Task GetProducts();
        public Task initCategories();
        public Task initManufactures();

        public void resetCat();
        public void resetMan();
    }
}