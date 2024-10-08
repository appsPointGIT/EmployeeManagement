using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Core.Models
{
    public class Department
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department cannot exceed 100 characters.")]
        public string DepartmentName { get; set; }
    }
}
