using EmployeeManagementApp.Core.Models;

namespace EmployeeManagementApp.Core.Interfaces
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department? GetDepartmentById(int id);
        void Add(Department department);
        void Update(Department department);
        void Delete(int id);
    }
}
