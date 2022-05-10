using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlazorAppEC.Shared
{
    public interface IManufactureManage
    {
        public List<Manufacture> Manufactures {get; set;}

        public Task initManufactures();
    }
}
