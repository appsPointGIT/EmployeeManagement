using EmployeeManagementApp.Core.DTOs;
using EmployeeManagementApp.Core.Interfaces;
using EmployeeManagementApp.Core.Models;

namespace EmployeeManagementApp.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _departmentRepository.GetAll();
        }

        public Department? GetDepartmentById(int id)
        {
            return _departmentRepository.GetDepartmentById(id);
        }

        public void AddDepartment(DepartmentDto departmentDto)
        {
            var department = GetMappedDepartment(departmentDto);
            _departmentRepository.Add(department);
        }

        public bool UpdateDepartment(int id, DepartmentDto departmentDto)
        {
            if (_departmentRepository.GetDepartmentById(id) == null)
                return false;

            var department = GetMappedDepartment(departmentDto, id);
            _departmentRepository.Update(department);
            return true;
        }

        public bool DeleteDepartment(int id)
        {
            if (_departmentRepository.GetDepartmentById(id) == null)
                return false;

            _departmentRepository.Delete(id);
            return true;
        }

        private Department GetMappedDepartment(DepartmentDto departmentDto, int id = 0)
        {
            return new Department()
            {
                Id = id,
                DepartmentName = departmentDto.DepartmentName,
            };
        }

    }
}
