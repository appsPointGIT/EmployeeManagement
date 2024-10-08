using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Core.Models
{
    public class Salary
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Basic Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Basic Salary must be a positive value.")]
        public decimal BasicSalary { get; set; } = 0;
    }
}
