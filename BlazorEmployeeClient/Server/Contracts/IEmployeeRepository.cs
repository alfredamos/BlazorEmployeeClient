using BlazorEmployeeClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Server.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {               
        Task<IEnumerable<HeadCounter>> DeptSearcher(Dept? searchKey = null);        
    }
}
