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
