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
    public class SQLAddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SQLAddressRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Address> AddEntity(Address newEntity)
        {
            var address = await _context.Addresses.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return address.Entity;
        }

        public async Task<Address> DeleteEntity(int id)
        {
            var addressToDeplete = await _context.Addresses.FindAsync(id);

            if (addressToDeplete != null)
            {
                _context.Addresses.Remove(addressToDeplete);
                await _context.SaveChangesAsync();
            }

            return addressToDeplete;
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> GetById(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<IEnumerable<Address>> Search(string searchKey)
        {
            IQueryable<Address> query = _context.Addresses;

            if (string.IsNullOrWhiteSpace(searchKey))
            {
                return await query.ToListAsync();
            }

            return await query.Where(x => x.City.Contains(searchKey) || x.Country.Contains(searchKey) ||
                              x.PostCode.Contains(searchKey) || x.State.Contains(searchKey) ||
                              x.Street.Contains(searchKey)).ToListAsync();
        }

        public async Task<Address> UpdateEntity(Address updatedEntity)
        {
            var result = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressID == updatedEntity.AddressID);

            _mapper.Map(updatedEntity, result);

            await _context.SaveChangesAsync();

            return result;
        }
    }
}
