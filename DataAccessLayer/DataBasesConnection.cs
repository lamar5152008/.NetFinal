using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    internal class DataBasesConnection
    {
        public static SqlConnection createMSSqlConnection()
        {
            string connectionString = @"Data Source=localhost; Initial Catalog= MarwaCar; Integrated Security=True";
            var conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
