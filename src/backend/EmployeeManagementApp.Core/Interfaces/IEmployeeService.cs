using EmployeeManagementApp.Core.DTOs;

namespace EmployeeManagementApp.Core.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDto? GetEmployeeById(int id);
        void AddEmployee(EmployeeInputDto employee);
        bool UpdateEmployee(int id, EmployeeInputDto employeeDto);
        bool DeleteEmployee(int id);
    }
}
