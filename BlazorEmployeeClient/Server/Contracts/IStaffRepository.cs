using BlazorEmployeeClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Server.Contracts
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        Task<IEnumerable<HeadCount>> DeptSearcher(string searchKey = null);
    }
}
