using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlazorAppEC.Shared
{
    public interface ICategoryManageVM
    {
        public List<Category> Categories {get; set;}

        public Task initCategory();
    }
}
