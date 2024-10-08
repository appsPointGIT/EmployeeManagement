using EmployeeManagementApp.Core.Interfaces;
using EmployeeManagementApp.Core.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementApp.DataAccess.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IDataHelper _dataHelper;

        public DepartmentRepository(IDataHelper dataHelper)
        {
            _dataHelper = dataHelper;
        }

        public IEnumerable<Department> GetAll()
        {
            string query = @"SELECT Id, DepartmentName FROM Departments ORDER BY Id;";

            DataTable dataTable = _dataHelper.ExecuteQuery(query);
            var departments = new List<Department>();

            foreach (DataRow row in dataTable.Rows)
            {
                departments.Add(new Department
                {
                    Id = (int)row["Id"],
                    DepartmentName = row["DepartmentName"].ToString() ?? string.Empty,
                });
            }

            return departments;
        }

        public Department? GetDepartmentById(int id)
        {
            string query = @" SELECT TOP 1 Id, DepartmentName 
                              FROM Departments
                              WHERE Id = @Id
                              ORDER BY Id;";

            SqlParameter[] parameters = {
                new SqlParameter("@Id", id)
            };

            DataTable dataTable = _dataHelper.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                return new Department
                {
                    Id = (int)row["Id"],
                    DepartmentName = row["DepartmentName"].ToString() ?? string.Empty,
                };
            }

            return null;
        }

        public void Add(Department department)
        {
            string query = @"INSERT INTO Departments (DepartmentName) 
                             VALUES (@DepartmentName);";

            SqlParameter[] parameters = {
                new SqlParameter("@DepartmentName", department.DepartmentName),
            };

            _dataHelper.ExecuteNonQuery(query, parameters);
        }

        public void Update(Department department)
        {
            string query = @"UPDATE Departments 
                             SET DepartmentName = @DepartmentName 
                             WHERE Id = @Id;";

            SqlParameter[] parameters = {
                new SqlParameter("@DepartmentName", department.DepartmentName),
                new SqlParameter("@Id", department.Id)
            };

            _dataHelper.ExecuteNonQuery(query, parameters);
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Departments WHERE Id = @Id;";
            SqlParameter[] parameters = {
                new SqlParameter("@Id", id)
            };

            _dataHelper.ExecuteNonQuery(query, parameters);
        }

    }
}
