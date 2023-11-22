using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterface;
using DataObject;
using System.Transactions;
namespace DataAccessLayer
{
    public class TransactionAccessor : TransactionAccessorInterface
    {
        public int insert(Trans transaction)
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

        public List<Trans> selectTransactionByCustomerId(int customerID)
        {
            List<Trans> transactions = new List<Trans>();
            SqlConnection conn = DataBasesConnection.createMSSqlConnection();
            var cmd = new SqlCommand("sp_select_transactions_by_customerId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@customerID", customerID);
            try
            {
                conn.Open();
               var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Trans trans = new Trans();
                        trans.ID = reader.GetInt32(0);
                        trans.CustomerId = reader.GetInt32(1);
                        trans.CarId = reader.GetInt32(2);
                        trans.Price = reader.GetString(3);
                        trans.Date = reader.GetString(4);
                        transactions.Add(trans);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally { conn.Close(); }
            return transactions;
        }

        
    }
}
