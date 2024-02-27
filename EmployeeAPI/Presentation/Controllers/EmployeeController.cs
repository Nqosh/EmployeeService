using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Interfaces;
using EmployeeAPI.Domain.Models;
using EmployeeAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/<Employee>
        [HttpGet("GetEmployees")]
        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
        {
            var employeeList = await _employeeService.GetEmployeesAsync();
            if (employeeList == null)
            {
                return Enumerable.Empty<EmployeeDTO>();
            }

            return employeeList.ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeAsync(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployeeAsync([FromBody] EmployeeCreateDTO employee)
        {
            if (employee == null)
                return BadRequest(ModelState);

            if (_employeeService.EmployeeExists(employee.FirstName))
            {
                ModelState.AddModelError("", "Employee Exists!");
                return StatusCode(404, ModelState);
            }

            if (await _employeeService.AddEmployeeAsync(employee)) return Ok();
            return BadRequest();
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.Id)
            {
                return BadRequest(ModelState);
            }

            if (await _employeeService.UpdateEmployeeAsync(id, employee)) return Ok();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            if (await _employeeService.DeleteEmployeeAsync(id)) return Ok();
            return NotFound();
        }
    }
}
