using EmployeeManagementApp.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Core.DTOs
{
    public class EmployeeDto
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(10, ErrorMessage = "Title cannot exceed 10 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Middle name cannot exceed 100 characters.")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "NIC number is required.")]
        [StringLength(15, ErrorMessage = "NIC number cannot exceed 15 characters.")]
        public string NICNumber { get; set; }

        [StringLength(10, ErrorMessage = "EPF number cannot exceed 10 characters.")]
        public string EPFNumber { get; set; }

        [StringLength(10, ErrorMessage = "ETF number cannot exceed 10 characters.")]
        public string ETFNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
        public string Gender { get; set; }

        public bool ActiveStatus { get; set; } = false;

        [Required(ErrorMessage = "Department is required.")]
        public Department Department { get; set; } = new();

        [Required(ErrorMessage = "Salary is required.")]
        public SalaryDto Salary { get; set; } = new();
    }

    public class EmployeeInputDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(10, ErrorMessage = "Title cannot exceed 10 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters.")]
        public string FirstName { get; set; }

        [StringLength(100, ErrorMessage = "Middle name cannot exceed 100 characters.")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "NIC number is required.")]
        [StringLength(15, ErrorMessage = "NIC number cannot exceed 15 characters.")]
        public string NICNumber { get; set; }

        [StringLength(10, ErrorMessage = "EPF number cannot exceed 10 characters.")]
        public string EPFNumber { get; set; }

        [StringLength(10, ErrorMessage = "ETF number cannot exceed 10 characters.")]
        public string ETFNumber { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
        public string Gender { get; set; }

        public bool ActiveStatus { get; set; } = false;

        [Required(ErrorMessage = "Department is required.")]
        public DepartmentInputDto Department { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public SalaryInputDto Salary { get; set; }
    }
}
