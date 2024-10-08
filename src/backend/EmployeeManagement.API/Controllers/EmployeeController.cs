using EmployeeManagementApp.Core.DTOs;
using EmployeeManagementApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                return Ok(_employeeService.GetAllEmployees());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employees.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employee by id.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public IActionResult AddEmployee([FromBody] EmployeeInputDto employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _employeeService.AddEmployee(employee);
                return Ok("Employee added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding employee.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, EmployeeInputDto employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_employeeService.UpdateEmployee(id, employee))
                {
                    return NotFound();
                }

                return Ok("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating employee.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                if (!_employeeService.DeleteEmployee(id))
                {
                    return NotFound();
                }

                return Ok("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting employee.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
