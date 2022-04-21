using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.Toast.Services;

namespace BlazorAppEC.Shared.Services
{
    public class CartService : ICartService
    {
        public List<Product> Carts { get; set; } = new List<Product>();
        const string CART = "cart";
        private readonly ISyncLocalStorageService _localStorage;
        private readonly IToastService _toastService;
        public CartService(ISyncLocalStorageService localStorage, IToastService toast)
        {
            _localStorage = localStorage;
            _toastService = toast;
            Carts = _localStorage.GetItem<List<Product>>(CART);
        }

        public void AddToCart(Product item)
        {
            // logic go here
            if (Carts == null)
            {
                item.Quantity = 1;
                Carts = new List<Product>() { item };
            }
            else
            {
                var cartItem = Carts.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (cartItem != null)
                {
                    cartItem.Quantity += 1;
                }
                else
                {
                    item.Quantity = 1;
                    Carts.Add(item);
                }
            }
            _localStorage.SetItem<List<Product>>(CART, Carts);
            _toastService.ShowSuccess(item.Name, "Added");
        }

        public void DeleteItem(Product item)
        {
            // logic go here
            if (Carts == null)
            {
                return;
            }
            else
            {
                var cartItem = Carts.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (cartItem != null)
                {
                    Carts.Remove(cartItem);
                }
                else
                {
                    return;
                }
            }
            _localStorage.SetItem<List<Product>>(CART, Carts);
            _toastService.ShowSuccess(item.Name, "Deleted");
        }

        public void IncreaseQuantity(Product item)
        {
            // logic go here
            if (Carts == null)
            {
                return;
            }
            else
            {
                var cartItem = Carts.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (cartItem != null)
                {
                    //call api check quantity;
                    cartItem.Quantity += 1;
                }
                else
                {
                    return;
                }
            }
            _localStorage.SetItem<List<Product>>(CART, Carts);
        }

        public void DecreaseQuantity(Product item)
        {
            // logic go here
            if (Carts == null)
            {
                return;
            }
            else
            {
                var cartItem = Carts.FirstOrDefault(p => p.ProductId == item.ProductId);
                if (cartItem != null)
                {
                    //call api check quantity;
                    cartItem.Quantity = cartItem.Quantity - 1 > 1 ? cartItem.Quantity - 1 : 1;
                }
                else
                {
                    return;
                }
            }
            _localStorage.SetItem<List<Product>>(CART, Carts);
        }

        public int GetTotalItems()
        {
            int totalQty = 0;
            foreach (var item in Carts)
            {   
                totalQty += item.Quantity;
            }

            return totalQty;
        }

        public int GetPrice()
        {
            int total = 0;
            foreach (var item in Carts)
            {   
                total += item.Quantity * item.Price;
            }

            return total;
        }

        public void ClearCart()
        {
            Carts.Clear();
            _localStorage.SetItem<List<Product>>(CART, Carts);
        }
    }
}