using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInerface;
using DataObject;

namespace DataAccessLayer
{
    public class EmployeeAccessor : EmployeeAccessorInterface
    {
        public List<string> getEmployeeRoles(int employeeID)
        {
           List<string> roles = new List<string>();
            // 1- connect to databases
            SqlConnection conn = DataBasesConnection.createMSSqlConnection();
            // 2- determain the store prosdure 
            var cmd = new SqlCommand("sp_select_roles_by_employeeID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            // 3- determain the parameters and values
            cmd.Parameters.AddWithValue("@EmployeeID ", employeeID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        
            
            return roles;
            
        }

        public int insertNewEmployee(EmployeeVM employee)
        {
            int employeeId = 0;
            SqlConnection conn = DataBasesConnection.createMSSqlConnection();
            var cmd = new SqlCommand("sp_insert_employee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@GivenName", employee.GivenName);
            cmd.Parameters.AddWithValue("@FamilyName", employee.FamilyName);
            cmd.Parameters.AddWithValue("@Phone", employee.Phone);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Password", employee.Password);
            cmd.Parameters.AddWithValue("@Active", employee.Active);
            cmd.Parameters.AddWithValue("@Role", employee.Roles[0]);
            try
            {
                conn.Open();
                employeeId = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return employeeId;
        }

        public List<Employee> selectAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            // 1- connect to databases
            SqlConnection conn = DataBasesConnection.createMSSqlConnection();
            // 2- determain the store prosdure 
            var cmd = new SqlCommand("sp_select_all_employees", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open ();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = reader.GetInt32(0);
                        employee.GivenName = reader.GetString(1);
                        employee.FamilyName = reader.GetString(2);
                        employee.Phone = reader.GetString(3);
                        employee.Email = reader.GetString(4);
                        employee.Password = reader.GetString(5);
                        employee.Active = reader.GetBoolean(6);
                        employees.Add(employee);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return employees;
        }

        public int verfiyUser(string userName, string password)
        {
            int result = 0;
            // 1- connect to databases
            SqlConnection conn = DataBasesConnection.createMSSqlConnection();
            
            // 2- determain the store prosdure 
            var cmd = new SqlCommand("sp_verify_user", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // 3- determain the parameters and values
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = reader.GetInt32(0);
                    } 
                   
                }
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
