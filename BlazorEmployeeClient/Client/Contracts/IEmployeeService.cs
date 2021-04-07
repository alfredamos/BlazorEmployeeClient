using BlazorEmployeeClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.Contracts
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        Task<IEnumerable<HeadCounter>> Searcher(Dept searchKey);
        Task<IEnumerable<HeadCounter>> Searcher();
       
    }
}
