using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.Contracts
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> AddEntity(T newEntity);
        Task<T> UpdateEntity(T updatedEntity);
        Task DeleteEntity(int id);
        Task<IEnumerable<T>> Search(string searchKey);
    }
}
