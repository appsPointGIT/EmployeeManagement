using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Core.DTOs
{
    public class SalaryDto
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Basic Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Basic Salary must be a positive value.")]
        public decimal BasicSalary { get; set; } = 0;
    }

    public class SalaryInputDto
    {
        [Required(ErrorMessage = "Basic Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Basic Salary must be a positive value.")]
        public decimal BasicSalary { get; set; } = 0;
    }
}
