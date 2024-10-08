using EmployeeManagementApp.Core.DTOs;
using EmployeeManagementApp.Core.Models;

namespace EmployeeManagementApp.Core.Interfaces
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAllDepartments();
        Department? GetDepartmentById(int id);
        void AddDepartment(DepartmentDto department);
        bool UpdateDepartment(int id, DepartmentDto department);
        bool DeleteDepartment(int id);
    }
}
