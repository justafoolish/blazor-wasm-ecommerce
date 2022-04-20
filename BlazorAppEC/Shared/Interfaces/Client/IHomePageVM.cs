using System.Threading.Tasks;
using System.Collections.Generic;
namespace BlazorAppEC.Shared
{
    public interface IHomePageVM
    {
        public List<Product> LatestProduct { get; set; }
        public List<Product> BestSeller { get; set; }
        public Product Product { get; set; }
        public Task<List<Product>> GetLatestProduct();
        public Task<List<Product>> GetBestSellerProduct();
        public Task GetProductDetail(string slug);
    }
}