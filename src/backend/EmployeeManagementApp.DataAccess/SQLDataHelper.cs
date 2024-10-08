using EmployeeManagementApp.Core.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementApp.DataAccess
{
    public class SQLDataHelper : IDataHelper
    {
        private readonly string _connectionString;

        public SQLDataHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query, IEnumerable<SqlParameter>? parameters = null)
        {
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
        }

        public int ExecuteNonQuery(string query, IEnumerable<SqlParameter>? parameters = null)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    connection.Open();
                    affectedRows = command.ExecuteNonQuery();
                }
            }

            return affectedRows;
        }

        public object ExecuteScalar(string query, IEnumerable<SqlParameter>? parameters = null)
        {
            object result;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    connection.Open();
                    result = command.ExecuteScalar();
                }
            }

            return result;
        }
    }
}
