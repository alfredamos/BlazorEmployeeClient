using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorEmployeeClient.Server.Data;
using BlazorEmployeeClient.Shared.Models;
using BlazorEmployeeClient.Server.Contracts;

namespace BlazorEmployeeClient.Server.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {        
        private readonly IAddressRepository _addressRepository;

        public AddressesController(IAddressRepository addressRepository)
        {            
            _addressRepository = addressRepository;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            try
            {
                return Ok(await _addressRepository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/Addresses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            try
            {
                var address = await _addressRepository.GetById(id);

                if (address == null)
                {
                    return NotFound($"Address with Id = {id} not found.");
                }

                return address;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
            
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Address>> PutAddress(int id, Address address)
        {
            try
            {
                if (id != address.AddressID)
                {
                    return BadRequest();
                }

                var addressToUpdate = await _addressRepository.GetById(id);

                if (addressToUpdate == null)
                {
                    return NotFound($"Address with Id = {id} not found.");
                }

                return await _addressRepository.UpdateEntity(address);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }
            
            
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest("Invalid input");
                }

                var createdAddress = await _addressRepository.AddEntity(address);

                return CreatedAtAction(nameof(GetAddress), new { id = createdAddress.AddressID }, createdAddress);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
           
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            try
            {
                var address = await _addressRepository.GetById(id);

                if (address == null)
                {
                    return NotFound($"Address with Id = {id} not found.");
                }

                return await _addressRepository.DeleteEntity(id);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
           
        }


        // GET: api/Addresses/search/search
        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<Address>>> Search(string search)
        {
            try
            {
                return Ok(await _addressRepository.Search(search));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

    }
}
