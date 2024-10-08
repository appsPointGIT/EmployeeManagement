using EmployeeManagementApp.Core.Interfaces;
using EmployeeManagementApp.Core.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementApp.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDataHelper _dataHelper;

        public EmployeeRepository(IDataHelper dataHelper)
        {
            _dataHelper = dataHelper;
        }

        public IEnumerable<(Employee Employee, Department Department, Salary Salary)> GetAll()
        {
            string query = @"
            SELECT e.Id, e.Title, e.FirstName, ISNULL(e.MiddleName, '') AS MiddleName, e.LastName, 
                   e.NICNumber, e.EPFNumber, e.ETFNumber, e.DateOfBirth, e.Gender, e.ActiveStatus, 
                   d.Id AS DepartmentId, d.DepartmentName, 
                   s.Id AS SalaryId, ISNULL(s.BasicSalary, 0.00) AS BasicSalary
            FROM Employees e
            LEFT JOIN Departments d ON e.DepartmentId = d.Id
            LEFT JOIN Salaries s ON e.Id = s.EmployeeId
            ORDER BY e.Id, e.FirstName;";

            var dataTable = _dataHelper.ExecuteQuery(query);

            return dataTable.AsEnumerable().Select(row => (
                Employee: new Employee
                {
                    Id = row.Field<int>("Id"),
                    Title = row.Field<string>("Title"),
                    FirstName = row.Field<string>("FirstName"),
                    MiddleName = row.Field<string>("MiddleName"),
                    LastName = row.Field<string>("LastName"),
                    NICNumber = row.Field<string>("NICNumber"),
                    EPFNumber = row.Field<string>("EPFNumber"),
                    ETFNumber = row.Field<string>("ETFNumber"),
                    DateOfBirth = row.Field<DateTime?>("DateOfBirth"),
                    Gender = row.Field<string>("Gender"),
                    ActiveStatus = row.Field<bool>("ActiveStatus"),
                    DepartmentId = row.Field<int>("DepartmentId")
                },
                Department: new Department
                {
                    Id = row.Field<int>("DepartmentId"),
                    DepartmentName = row.Field<string>("DepartmentName")
                },
                Salary: new Salary
                {
                    Id = row.Field<int?>("SalaryId"),
                    BasicSalary = row.Field<decimal>("BasicSalary"),
                    EmployeeId = row.Field<int>("Id")
                }
            )).ToList();
        }

        public (Employee Employee, Department Department, Salary Salary)? GetEmployeeById(int id)
        {
            string query = @"
            SELECT TOP 1 e.Id, e.Title, e.FirstName, ISNULL(e.MiddleName, '') AS MiddleName, e.LastName, 
                   e.NICNumber, e.EPFNumber, e.ETFNumber, e.DateOfBirth, e.Gender, e.ActiveStatus, 
                   d.Id AS DepartmentId, d.DepartmentName, 
                   s.Id AS SalaryId, ISNULL(s.BasicSalary, 0.00) AS BasicSalary
            FROM Employees e
            LEFT JOIN Departments d ON e.DepartmentId = d.Id
            LEFT JOIN Salaries s ON e.Id = s.EmployeeId
            WHERE e.Id = @Id 
            ORDER BY e.Id, e.FirstName; ";

            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            var dataTable = _dataHelper.ExecuteQuery(query, parameters).AsEnumerable();
            var employeeData = dataTable.FirstOrDefault();

            if (employeeData == null) return null;

            return (
                Employee: new Employee
                {
                    Id = employeeData.Field<int>("Id"),
                    Title = employeeData.Field<string>("Title"),
                    FirstName = employeeData.Field<string>("FirstName"),
                    MiddleName = employeeData.Field<string>("MiddleName"),
                    LastName = employeeData.Field<string>("LastName"),
                    NICNumber = employeeData.Field<string>("NICNumber"),
                    EPFNumber = employeeData.Field<string>("EPFNumber"),
                    ETFNumber = employeeData.Field<string>("ETFNumber"),
                    DateOfBirth = employeeData.Field<DateTime?>("DateOfBirth"),
                    Gender = employeeData.Field<string>("Gender"),
                    ActiveStatus = employeeData.Field<bool>("ActiveStatus"),
                    DepartmentId = employeeData.Field<int>("DepartmentId")
                },
                Department: new Department
                {
                    Id = employeeData.Field<int>("DepartmentId"),
                    DepartmentName = employeeData.Field<string>("DepartmentName")
                },
                Salary: new Salary
                {
                    Id = employeeData.Field<int?>("SalaryId"),
                    BasicSalary = employeeData.Field<decimal>("BasicSalary"),
                    EmployeeId = employeeData.Field<int>("Id")
                }
            );
        }

        public void Add(Employee employee, Salary salary)
        {
            string insertEmployeeQuery = @"
                INSERT INTO Employees 
                (Title, FirstName, MiddleName, LastName, NICNumber, EPFNumber, ETFNumber, DateOfBirth, Gender, DepartmentId, ActiveStatus)
                VALUES 
                (@Title, @FirstName, @MiddleName, @LastName, @NICNumber, @EPFNumber, @ETFNumber, @DateOfBirth, @Gender, @DepartmentId, @ActiveStatus);
                SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@Title", employee.Title),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@MiddleName", employee.MiddleName ?? string.Empty),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@NICNumber", employee.NICNumber),
                new SqlParameter("@EPFNumber", employee.EPFNumber ?? string.Empty),
                new SqlParameter("@ETFNumber", employee.ETFNumber ?? string.Empty),
                new SqlParameter("@DateOfBirth", employee.DateOfBirth),
                new SqlParameter("@Gender", employee.Gender),
                new SqlParameter("@DepartmentId", employee.DepartmentId),
                new SqlParameter("@ActiveStatus", employee.ActiveStatus)
            };

            int employeeId = Convert.ToInt32(_dataHelper.ExecuteScalar(insertEmployeeQuery, parameters));
            if (employeeId > 0)
            {
                string insertSalaryQuery = @"
                    INSERT INTO Salaries (EmployeeId, BasicSalary)
                    VALUES (@EmployeeId, @BasicSalary)";

                SqlParameter[] salaryParameters = {
                    new SqlParameter("@EmployeeId", employeeId),
                    new SqlParameter("@BasicSalary", salary.BasicSalary)
                };

                _dataHelper.ExecuteNonQuery(insertSalaryQuery, salaryParameters);
            }
        }

        public void Update(Employee employee, Salary salary)
        {
            string updateEmployeeQuery = @"
            UPDATE Employees
            SET Title = @Title, FirstName = @FirstName, MiddleName = @MiddleName, 
                LastName = @LastName, NICNumber = @NICNumber, EPFNumber = @EPFNumber, ETFNumber = @ETFNumber, 
                DateOfBirth = @DateOfBirth, Gender = @Gender, ActiveStatus = @ActiveStatus, 
                DepartmentId = @DepartmentId
            WHERE Id = @Id;";

            SqlParameter[] parameters = {
                new SqlParameter("@Id", employee.Id),
                new SqlParameter("@Title", employee.Title),
                new SqlParameter("@FirstName", employee.FirstName),
                new SqlParameter("@MiddleName", employee.MiddleName ?? string.Empty),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@NICNumber", employee.NICNumber),
                new SqlParameter("@EPFNumber", employee.EPFNumber ?? string.Empty),
                new SqlParameter("@ETFNumber", employee.ETFNumber ?? string.Empty),
                new SqlParameter("@DateOfBirth", employee.DateOfBirth),
                new SqlParameter("@Gender", employee.Gender),
                new SqlParameter("@ActiveStatus", employee.ActiveStatus),
                new SqlParameter("@DepartmentId", employee.DepartmentId)
            };

            int rowsAffected = _dataHelper.ExecuteNonQuery(updateEmployeeQuery, parameters);
            if (rowsAffected > 0)
            {
                SqlParameter[] setSalaryParams = {
                    new SqlParameter("@BasicSalary", salary.BasicSalary),
                    new SqlParameter("@EmployeeId", employee.Id)
                };

                string checkSalaryQuery = @"SELECT COUNT(1) FROM Salaries WHERE EmployeeId = @EmployeeId;";
                SqlParameter[] checkSalaryParams = { new SqlParameter("@EmployeeId", employee.Id) };
                int salaryExists = Convert.ToInt32(_dataHelper.ExecuteScalar(checkSalaryQuery, checkSalaryParams));
                if (salaryExists > 0)
                {
                    string updateSalaryQuery = @"
                    UPDATE Salaries
                    SET BasicSalary = @BasicSalary
                    WHERE EmployeeId = @EmployeeId;";

                    _dataHelper.ExecuteNonQuery(updateSalaryQuery, setSalaryParams);
                }
                else
                {
                    string insertSalaryQuery = @"
                    INSERT INTO Salaries (EmployeeId, BasicSalary)
                    VALUES (@EmployeeId, @BasicSalary)";

                    _dataHelper.ExecuteNonQuery(insertSalaryQuery, setSalaryParams);
                }
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Employees WHERE Id = @Id";
            SqlParameter[] parameters = {
                new SqlParameter("@Id", id)
            };

            _dataHelper.ExecuteNonQuery(query, parameters);
        }

    }
}
