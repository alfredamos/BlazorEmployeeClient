using AutoMapper;
using BlazorEmployeeClient.Server.Contracts;
using BlazorEmployeeClient.Server.Data;
using BlazorEmployeeClient.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Server.SQL
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context; 
     
        public SQLEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;           
        }
        public async Task<Employee> AddEntity(Employee newEntity)
        {
            var department = await _context.Employees.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return department.Entity;
        }

        public async Task<Employee> DeleteEntity(int id)
        {
            var employeeToDeplete = await _context.Employees.FindAsync(id);

            if (employeeToDeplete != null)
            {
                _context.Employees.Remove(employeeToDeplete);
                await _context.SaveChangesAsync();
            }

            return employeeToDeplete;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Employees.Include(x => x.Addresses)
                                 .ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _context.Employees.Include(x => x.Addresses)                                 
                                 .FirstOrDefaultAsync(x => x.EmployeeID == id);
        }

        public async Task<IEnumerable<Employee>> Search(string searchKey)
        {
            Enum.TryParse(searchKey, out Dept SearchDept);
            Enum.TryParse(searchKey, out Gender SearchGender);

            IQueryable<Employee> employees = _context.Employees.Include(x => x.Addresses.
                                             Where(x => x.City.Contains(searchKey) ||
                                             x.Country.Contains(searchKey) || x.PostCode.Contains(searchKey)
                                             || x.State.Contains(searchKey) || x.Street.Contains(searchKey)));

            if (string.IsNullOrWhiteSpace(searchKey))
            {
                return await employees.ToListAsync();
            }

            return await employees.Where(x => x.Email.Contains(searchKey) || x.FullName.Contains(searchKey) ||
                         x.PhoneNumber.Contains(searchKey) || x.Department.Equals(SearchDept) ||
                         x.Gender.Equals(SearchGender)).ToListAsync();
        }

        public async Task<IEnumerable<HeadCounter>> DeptSearcher(Dept? searchKey = null)
        {
            return await DeptSearcherHelper(searchKey);
        }

        private async Task<IEnumerable<HeadCounter>> DeptSearcherHelper(Dept? searchKey = null)
        {
            var headCounters = new List<HeadCounter>();

            var employees = await _context.Employees.ToListAsync();
            var GroupByDepartments = searchKey.HasValue ? employees
                                     .Where(x => x.Department.Equals(searchKey))                                     
                                     .GroupBy(x => x.Department)
                                     .Select(g => new { Department = g.Key, Count = g.Count() })     :
                                      employees                                    
                                     .GroupBy(x => x.Department)
                                     .Select(g => new { Department = g.Key, Count = g.Count() });           

            foreach (var group in GroupByDepartments)
            {
                    var headCounter = new HeadCounter
                    {
                        Department = group.Department,
                        Count = group.Count                        
                    };
                    headCounters.Add(headCounter);
            }

            return headCounters;
        }

        public async Task<Employee> UpdateEntity(Employee updatedEntity)
        {           
            var result = _context.Employees.Attach(updatedEntity);
            result.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return updatedEntity;
        }

    }
}
