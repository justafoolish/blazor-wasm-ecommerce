using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAppEC.Shared {
    public interface IOrderDetailVM {
        List<OrderDetail> OrderDetail {get; set;}
        Order Order {get;set;}
        Discount Discount {get;set;}
        User User {get;set;}
        Task InitOrder(int order_id);
        Task<bool> ConfirmOrder(int order_id);
    }
}