using AutoMapper;
using BlazorEmployeeClient.Server.Contracts;
using BlazorEmployeeClient.Server.Helpers;
using BlazorEmployeeClient.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Server.Controllers.Staffs
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public StaffsController(IStaffRepository staffRepository, IMapper mapper,
                                   IFileStorageService filestorageService)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
            _fileStorageService = filestorageService;
        }

        // GET: api/Staffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaffs()
        {
            try
            {
                return Ok(await _staffRepository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        // GET: api/Staffs/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            try
            {
                var staff = await _staffRepository.GetById(id);

                if (staff == null)
                {
                    return NotFound($"Staff with Id = {id} not found.");
                }

                return staff;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }

        }

        // PUT: api/Staffs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Staff>> PutStaff(int id, Staff staff)
        {
            try
            {
                if (id != staff.StaffID)
                {
                    return BadRequest("Id mismatch.");
                }

                var staffToUpdate = await _staffRepository.GetById(id);

                if (staffToUpdate == null)
                {
                    return NotFound($"Staff with Id = {id} not found.");
                }

                if (!string.IsNullOrWhiteSpace(staff.PhotoPath))
                {
                    var staffPhoto = Convert.FromBase64String(staff.PhotoPath);
                    staff.PhotoPath = await _fileStorageService.EditFile(staffPhoto, "jpg", "staff", staff.PhotoPath);
                }

                _mapper.Map(staff, staffToUpdate);

                return await _staffRepository.UpdateEntity(staffToUpdate);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }


        }

        // POST: api/Staffs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            try
            {
                if (staff == null)
                {
                    return BadRequest("Invalid input");
                }

                if (!string.IsNullOrWhiteSpace(staff.PhotoPath))
                {
                    var staffPhoto = Convert.FromBase64String(staff.PhotoPath);
                    staff.PhotoPath = await _fileStorageService.SaveFile(staffPhoto, "jpg", "staff");
                }

                var createdStaff = await _staffRepository.AddEntity(staff);

                return CreatedAtAction(nameof(GetStaff), new { id = createdStaff.StaffID }, createdStaff);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data");
            }

        }

        // DELETE: api/Staffs/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Staff>> DeleteStaff(int id)
        {
            try
            {
                var staff = await _staffRepository.GetById(id);

                if (staff == null)
                {
                    return NotFound($"Staff with Id = {id} not found.");
                }

                return await _staffRepository.DeleteEntity(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }

        }

        // GET: api/Staffs/search/searchKey
        [HttpGet("search/{searchKey}")]
        public async Task<ActionResult<IEnumerable<Staff>>> Search(string searchKey)
        {
            try
            {
                return Ok(await _staffRepository.Search(searchKey));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        // GET: api/Staffs/department/search
        [HttpGet("department/{search}")]
        public async Task<ActionResult<IEnumerable<HeadCount>>> Searcher(string search)
        {
            try
            {
                return Ok(await _staffRepository.DeptSearcher(search));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        // GET: api/Staffs/department/search
        [HttpGet("dello")]
        public async Task<ActionResult<IEnumerable<HeadCount>>> Searchera()
        {
            try
            {
                return Ok(await _staffRepository.DeptSearcher());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

    }
}
