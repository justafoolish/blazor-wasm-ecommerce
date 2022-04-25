using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorAppEC.Shared.Authentication;

namespace BlazorAppEC.Shared
{
    public interface IAddReceivedNote
    {
        User User { get; set; }
        ReceivedNoteDetail ReceivedNoteItem { get; set; }
        List<Product> Products { get; set; }
        List<Supplier> Suppliers { get; set; }
        ReceivedNote ReceivedNote { get; set; }
        Task LoadProducts();
        void AddReceivedNoteDetail();
        Task LoadSuppliers();
        Task<bool> CreateReceivedNote();
        void DeleteReceiveNoteItem(int ProductID);
    }
}
