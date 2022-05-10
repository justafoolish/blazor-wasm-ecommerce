using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BlazorAppEC.Shared.Authentication;

namespace BlazorAppEC.Shared
{
    public interface IReceivedNoteVM
    {
        public int itemPerPage { get; set; }
        public int totalPage { get; set; }
        public int currentPage { get; set; }
        public List<ReceivedNote> ReceivedNotes { get; set; }
        public Task GetGoodReceivedNotes();
        public double GetBillTotal(List<ReceivedNoteDetail> receivedItems);
    }
}
