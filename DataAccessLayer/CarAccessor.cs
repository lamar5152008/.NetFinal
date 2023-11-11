using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;
using DataAccessInterface;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class CarAccessor : CarAccessorInterface
    {
        public List<Car> selectCars()
        {
            List<Car> cars = new List<Car>();
            SqlConnection conn = DataBasesConnection.createMSSqlConnection();
            var cmd = new SqlCommand("sp_select_all_cars", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Car car = new Car();
                        car.CarID = reader.GetInt32(0);
                        car.Name = reader.GetString(1);
                        car.Model = reader.GetString(2);
                        car.Color = reader.GetString(3);
                        car.Year = reader.GetString(4);
                        car.Active = reader.GetBoolean(5);  
                        cars.Add(car);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return cars;
        }
    }
}
