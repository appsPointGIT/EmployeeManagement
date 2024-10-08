using EmployeeManagementApp.Core.DTOs;
using EmployeeManagementApp.Core.Models;

namespace EmployeeManagementApp.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<(Employee Employee, Department Department, Salary Salary)> GetAll();
        (Employee Employee, Department Department, Salary Salary)? GetEmployeeById(int id);
        void Add(Employee employee, Salary salary);
        void Update(Employee employee, Salary salary);
        void Delete(int id);
    }
}
