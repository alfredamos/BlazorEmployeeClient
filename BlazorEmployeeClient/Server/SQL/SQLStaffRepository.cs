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
    public class SQLStaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;

        public SQLStaffRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Staff> AddEntity(Staff newEntity)
        {
            var department = await _context.Staffs.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return department.Entity;
        }

        public async Task<Staff> DeleteEntity(int id)
        {
            var employeeToDeplete = await _context.Staffs.FindAsync(id);

            if (employeeToDeplete != null)
            {
                _context.Staffs.Remove(employeeToDeplete);
                await _context.SaveChangesAsync();
            }

            return employeeToDeplete;
        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            return await _context.Staffs.Include(x => x.Addresses)
                                 .Include(x => x.Department).ToListAsync();
        }

        public async Task<Staff> GetById(int id)
        {
            return await _context.Staffs.Include(x => x.Addresses).Include(x => x.Department)
                                 .FirstOrDefaultAsync(x => x.StaffID == id);
        }

        public async Task<IEnumerable<Staff>> Search(string searchKey)
        {           
            Enum.TryParse(searchKey, out Gender SearchGender);

            IQueryable<Staff> employees = _context.Staffs.Include(x => x.Department).Include(x => x.Addresses.
                                             Where(x => x.City.Contains(searchKey) ||
                                             x.Country.Contains(searchKey) || x.PostCode.Contains(searchKey)
                                             || x.State.Contains(searchKey) || x.Street.Contains(searchKey)));

            if (string.IsNullOrWhiteSpace(searchKey))
            {
                return await employees.ToListAsync();
            }

            return await employees.Where(x => x.Email.Contains(searchKey) || x.FullName.Contains(searchKey) ||
                         x.PhoneNumber.Contains(searchKey) || x.Department.DepartmentName.Contains(searchKey) ||
                         x.Gender.Equals(SearchGender)).ToListAsync();
        }

        public async Task<IEnumerable<HeadCount>> DeptSearcher(string searchKey = null)
        {
            return await DeptSearcherHelper(searchKey);
        }

        private async Task<IEnumerable<HeadCount>> DeptSearcherHelper(string searchKey = null)
        {
            var headCounts = new List<HeadCount>();

            var employees = await _context.Staffs.Include(x => x.Addresses).Include(x => x.Department)
                                                 .ToListAsync();
            var GroupByDepartments = !string.IsNullOrWhiteSpace(searchKey) ? employees
                                     .Where(x => x.Department.DepartmentName.Contains(searchKey))
                                     .GroupBy(x => x.Department)
                                     .Select(g => new { Department = g.Key, Count = g.Count() }) :
                                      employees
                                     .GroupBy(x => x.Department)
                                     .Select(g => new { Department = g.Key, Count = g.Count() });

            foreach (var group in GroupByDepartments)
            {
                var headCount = new HeadCount
                {
                    Department = group.Department,
                    Count = group.Count
                };
                headCounts.Add(headCount);
            }

            return headCounts;
        }

        public async Task<Staff> UpdateEntity(Staff updatedEntity)
        {
            var result = _context.Staffs.Attach(updatedEntity);
            result.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return updatedEntity;
        }

    }
}
