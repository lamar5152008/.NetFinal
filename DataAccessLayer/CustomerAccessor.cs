using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterface;
using DataObject;

namespace DataAccessLayer
{
    public class CustomerAccessor : CustomerAccessorInterface
    {
        public int insertCustomer(Customer customer)
        {
            int result = 0;
            SqlConnection conn = DataBasesConnection.createMSSqlConnection();
            var cmd = new SqlCommand("sp_insert_customer", conn);
            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            cmd.Parameters.AddWithValue("@Email", customer.Email);
            cmd.Parameters.AddWithValue("@Phone", customer.Phone);
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

        public List<Customer> selectCustomers()
        {
           List<Customer> customers = new List<Customer>();
            SqlConnection conn = DataBasesConnection.createMSSqlConnection();
            var cmd = new SqlCommand("sp_select_customers", conn);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.CustomerID = reader.GetInt32(0);
                        customer.FirstName = reader.GetString(1);
                        customer.LastName = reader.GetString(2);
                        customer.Email = reader.GetString(3);
                        customer.Phone = reader.GetString(4);
                        customers.Add(customer);
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return customers;
        }
    }
}
