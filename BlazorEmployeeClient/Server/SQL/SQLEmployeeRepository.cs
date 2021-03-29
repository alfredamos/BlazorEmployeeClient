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
        private readonly IMapper _mapper;

        public SQLEmployeeRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
                                 .Include(x => x.Department).ToListAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _context.Employees.Include(x => x.Addresses)
                                 .Include(x => x.Department)
                                 .FirstOrDefaultAsync(x => x.EmployeeID == id);
        }

        public async Task<IEnumerable<Employee>> Search(string searchKey)
        {
            IQueryable<Employee> query = _context.Employees;

            if (string.IsNullOrWhiteSpace(searchKey))
            {
                return await query.ToListAsync();
            }

            return await query.Include(x => x.Addresses.Where(x => x.City.Contains(searchKey) ||
                         x.Country.Contains(searchKey) || x.PostCode.Contains(searchKey) ||
                         x.State.Contains(searchKey) || x.Street.Contains(searchKey)))
                         .Include(x => x.Department).Where(x => x.Department.DepartmentName.Contains(searchKey) 
                         || x.Email.Contains(searchKey) || x.FullName.Contains(searchKey) ||
                         x.PhoneNumber.Contains(searchKey)).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Search(Department department)
        {
            IQueryable<Employee> query = _context.Employees;

            if (department == null)
            {
                return await query.ToListAsync();
            }

            return await query.Where(x => x.Department.Equals(department)).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Search(Gender gender)
        {
            IQueryable<Employee> query = _context.Employees;

            return await query.Where(x => x.Department.Equals(gender)).ToListAsync();
        }

        public async Task<Employee> UpdateEntity(Employee updatedEntity)
        {
            var result = await _context.Employees.FirstOrDefaultAsync(x => x.DepartmentID == updatedEntity.DepartmentID);

            _mapper.Map(updatedEntity, result);

            await _context.SaveChangesAsync();

            return result;
        }
    }
}
