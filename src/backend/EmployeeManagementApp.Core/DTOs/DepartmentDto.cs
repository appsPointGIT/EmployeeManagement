using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Core.DTOs
{
    public class DepartmentDto
    {
        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters.")]
        public string DepartmentName { get; set; }
    }

    public class DepartmentInputDto
    {
        [Required(ErrorMessage = "Department id is required.")]
        public int Id { get; set; }
    }
}
