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

namespace BlazorEmployeeClient.Server.Controllers.Employees
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {        
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {           
            _employeeRepository = employeeRepository;
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

                return await _employeeRepository.UpdateEntity(employee);
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

                var createdEmployee = await _employeeRepository.AddEntity(employee);

                return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeID }, createdEmployee);

            }
            catch (Exception)
            {

                throw;
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

        // GET: api/Employees/search/search
        [HttpGet("search/{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string search)
        {
            try
            {
                return Ok(await _employeeRepository.Search(search));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

        // GET: api/Employees/department/search
        [HttpGet("department/{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(Department search)
        {
            try
            {
                return Ok(await _employeeRepository.Search(search));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data");
            }
        }

    }
}
