using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterface;
using DataObject;
namespace DataAccessLayer
{
    public class TransactionAccessor : TransactionAccessorInterface
    {
        public int insert(Transaction transaction)
        {
            int result = 0;
            SqlConnection conn = DataBasesConnection.createMSSqlConnection();
            var cmd = new SqlCommand("sp_insert_transaction", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerId", transaction.CustomerId);
            cmd.Parameters.AddWithValue("@CarId", transaction.CarId);
            cmd.Parameters.AddWithValue("@Price", transaction.Price);
            cmd.Parameters.AddWithValue("@Date", transaction.Date);
            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return result;
        }
    }
}
