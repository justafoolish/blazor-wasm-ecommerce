using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlazorAppEC.Shared
{
    public interface IDiscountManageVM
    {
        public List<Discount> Discounts {get; set;}

        public Task initDiscounts();
    }
}
