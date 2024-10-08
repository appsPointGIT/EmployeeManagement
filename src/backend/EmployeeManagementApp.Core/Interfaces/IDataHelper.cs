using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeManagementApp.Core.Interfaces
{
    public interface IDataHelper
    {
        DataTable ExecuteQuery(string query, IEnumerable<SqlParameter>? parameters = null);
        int ExecuteNonQuery(string query, IEnumerable<SqlParameter>? parameters = null);
        object ExecuteScalar(string query, IEnumerable<SqlParameter>? parameters = null);
    }
}
