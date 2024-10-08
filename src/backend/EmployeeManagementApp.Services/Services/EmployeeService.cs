using EmployeeManagementApp.Core.DTOs;
using EmployeeManagementApp.Core.Interfaces;
using EmployeeManagementApp.Core.Models;

namespace EmployeeManagementApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employeeData = _employeeRepository.GetAll();

            return employeeData.Select(data => new EmployeeDto
            {
                Id = data.Employee.Id,
                Title = data.Employee.Title,
                FirstName = data.Employee.FirstName,
                MiddleName = data.Employee.MiddleName,
                LastName = data.Employee.LastName,
                NICNumber = data.Employee.NICNumber,
                EPFNumber = data.Employee.EPFNumber,
                ETFNumber = data.Employee.ETFNumber,
                DateOfBirth = data.Employee.DateOfBirth,
                Gender = data.Employee.Gender,
                ActiveStatus = data.Employee.ActiveStatus,
                Department = new Department
                {
                    Id = data.Department.Id,
                    DepartmentName = data.Department.DepartmentName
                },
                Salary = new SalaryDto
                {
                    Id = data.Salary.Id,
                    BasicSalary = data.Salary.BasicSalary
                }
            }).ToList();
        }

        public EmployeeDto? GetEmployeeById(int id)
        {
            var employeeData = _employeeRepository.GetEmployeeById(id);

            if (!employeeData.HasValue || employeeData.GetValueOrDefault().Employee == null) return null;

            return new EmployeeDto
            {
                Id = employeeData.GetValueOrDefault().Employee.Id,
                Title = employeeData.GetValueOrDefault().Employee.Title,
                FirstName = employeeData.GetValueOrDefault().Employee.FirstName,
                MiddleName = employeeData.GetValueOrDefault().Employee.MiddleName,
                LastName = employeeData.GetValueOrDefault().Employee.LastName,
                NICNumber = employeeData.GetValueOrDefault().Employee.NICNumber,
                EPFNumber = employeeData.GetValueOrDefault().Employee.EPFNumber,
                ETFNumber = employeeData.GetValueOrDefault().Employee.ETFNumber,
                DateOfBirth = employeeData.GetValueOrDefault().Employee.DateOfBirth,
                Gender = employeeData.GetValueOrDefault().Employee.Gender,
                ActiveStatus = employeeData.GetValueOrDefault().Employee.ActiveStatus,
                Department = new Department
                {
                    Id = employeeData.GetValueOrDefault().Department.Id,
                    DepartmentName = employeeData.GetValueOrDefault().Department.DepartmentName
                },
                Salary = new SalaryDto
                {
                    Id = employeeData.GetValueOrDefault().Salary.Id,
                    BasicSalary = employeeData.GetValueOrDefault().Salary.BasicSalary
                }
            };
        }

        public void AddEmployee(EmployeeInputDto employeeDto)
        {
            var employee = GetMappedEmployee(employeeDto);
            var salary = GetMappedSalary(employeeDto);

            _employeeRepository.Add(employee, salary);
        }

        public bool UpdateEmployee(int id, EmployeeInputDto employeeDto)
        {
            if (_employeeRepository.GetEmployeeById(id) == null)
                return false;

            var employee = GetMappedEmployee(employeeDto, id);
            var salary = GetMappedSalary(employeeDto, id);

            _employeeRepository.Update(employee, salary);
            return true;
        }

        public bool DeleteEmployee(int id)
        {
            if (_employeeRepository.GetEmployeeById(id) == null)
                return false;

            _employeeRepository.Delete(id);
            return true;
        }

        private Employee GetMappedEmployee(EmployeeInputDto employeeDto, int id = 0)
        {
            return new Employee
            {
                Id = id,
                Title = employeeDto.Title,
                FirstName = employeeDto.FirstName,
                MiddleName = employeeDto.MiddleName,
                LastName = employeeDto.LastName,
                EPFNumber = employeeDto.EPFNumber,
                ETFNumber = employeeDto.ETFNumber,
                NICNumber = employeeDto.NICNumber,
                Gender = employeeDto.Gender,
                DateOfBirth = employeeDto.DateOfBirth,
                ActiveStatus = employeeDto.ActiveStatus,
                DepartmentId = employeeDto.Department.Id,
            };
        }

        private Salary GetMappedSalary(EmployeeInputDto employeeDto, int id = 0)
        {
            return new Salary
            {
                Id = 0,
                EmployeeId = id,
                BasicSalary = employeeDto.Salary.BasicSalary,
            };
        }

    }
}
