
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAppEC.Shared.Services {
    public interface ICartService {
        public List<Product> Carts { get;set; }
        public int GetTotalItems();
        public int GetPrice();
        public void AddToCart(Product item);
        public void DeleteItem(Product item);
        public void IncreaseQuantity(Product item);
        public void DecreaseQuantity(Product item);
        public void ClearCart();
        
    }
}