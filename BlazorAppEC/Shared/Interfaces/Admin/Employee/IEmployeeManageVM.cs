using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace BlazorAppEC.Shared
{
    public interface IEmployeeManageVM
    {
        public List<User> Employees {get; set;}

        public Task initEmployees();
    }
}
