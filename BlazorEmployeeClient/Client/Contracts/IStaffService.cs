using BlazorEmployeeClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.Contracts
{
    public interface IStaffService : IBaseService<Staff>
    {
        Task<IEnumerable<HeadCount>> Searcher(string searchKey);
        Task<IEnumerable<HeadCount>> Searcher();
    }
}
