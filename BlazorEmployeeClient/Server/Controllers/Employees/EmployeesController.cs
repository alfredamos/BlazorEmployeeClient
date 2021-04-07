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
using AutoMapper;
using BlazorEmployeeClient.Server.Helpers;

namespace BlazorEmployeeClient.Server.Controllers.Employees
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper,
                                   IFileStorageService filestorageService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _fileStorageService = filestorageService;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            try
            {
                return Ok(await _employeeRepository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        // GET: api/Employees/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetById(id);

                if (employee == null)
                {
                    return NotFound($"Employee with Id = {id} not found.");
                }

                return employee;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }

        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> PutEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeID)
                {
                    return BadRequest("Id mismatch.");
                }

                var employeeToUpdate = await _employeeRepository.GetById(id);

                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with Id = {id} not found.");
                }

                if (!string.IsNullOrWhiteSpace(employee.PhotoPath))
                {
                    var employeePhoto = Convert.FromBase64String(employee.PhotoPath);
                    employee.PhotoPath = await _fileStorageService.EditFile(employeePhoto, "jpg", "employee", employee.PhotoPath);
                }

                _mapper.Map(employee, employeeToUpdate);

                return await _employeeRepository.UpdateEntity(employeeToUpdate);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }


        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest("Invalid input");
                }

                if (!string.IsNullOrWhiteSpace(employee.PhotoPath))
                {
                    var employeePhoto = Convert.FromBase64String(employee.PhotoPath);
                    employee.PhotoPath = await _fileStorageService.SaveFile(employeePhoto, "jpg", "employee");
                }

                var createdEmployee = await _employeeRepository.AddEntity(employee);

                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeID }, createdEmployee);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data");
            }

        }

        // DELETE: api/Employees/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetById(id);

                if (employee == null)
                {
                    return NotFound($"Employee with Id = {id} not found.");
                }

                return await _employeeRepository.DeleteEntity(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }

        }

        // GET: api/Employees/search/searchKey
        [HttpGet("search/{searchKey}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string searchKey)
        {            
            try
            {                
                return Ok(await _employeeRepository.Search(searchKey));
            }
            catch (Exception)
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        // GET: api/Employees/department/search
        [HttpGet("department/{search}")]
        public async Task<ActionResult<IEnumerable<HeadCounter>>> Searcher(Dept? search)
        {
            try
            {
                return Ok(await _employeeRepository.DeptSearcher(search));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        // GET: api/Employees/department/search
        [HttpGet("dello")]
        public async Task<ActionResult<IEnumerable<HeadCounter>>> Searchera()
        {
            try
            {
                return Ok(await _employeeRepository.DeptSearcher());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }
                   
    }
}
