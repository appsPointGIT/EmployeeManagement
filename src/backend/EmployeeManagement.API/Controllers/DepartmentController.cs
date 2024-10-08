using EmployeeManagementApp.Core.DTOs;
using EmployeeManagementApp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentservice;
        private readonly ILogger<DepartmentController> _logger;

        public DepartmentController(IDepartmentService departmentservice, ILogger<DepartmentController> logger)
        {
            _departmentservice = departmentservice;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            try
            {
                return Ok(_departmentservice.GetAllDepartments());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving departments.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var department = _departmentservice.GetDepartmentById(id);
                if (department == null)
                {
                    return NotFound();
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving department by id.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public IActionResult AddDepartment([FromBody] DepartmentDto department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _departmentservice.AddDepartment(department);
                return Ok("Department added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding department.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartment(int id, DepartmentDto department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_departmentservice.UpdateDepartment(id, department))
                {
                    return NotFound();
                }

                return Ok("Department updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating department.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                if (!_departmentservice.DeleteDepartment(id))
                {
                    return NotFound();
                }

                return Ok("Department deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting department.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
