using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterface;
namespace DataAccessFake
{
    public class FakeCarAccessor : CarAccessorInterface
    {
        private List<Car> cars;
        private Car car;
        public FakeCarAccessor() {
            cars = new List<Car>();
            car = new Car();
            createSampleData();
            
        }

        private void createSampleData()
        {
            car.CarID = 1;
            car.Name = "car1";
            car.Model = "model1";
            car.Color = "color1";
            car.Year = "year1";
            car.Active = true;
            cars.Add(car);

            car = new Car();
            car.CarID = 2;
            car.Name = "car2";
            car.Model = "model2";
            car.Color = "color2";
            car.Year = "year2";
            car.Active = true;
            cars.Add(car);

            car = new Car();
            car.CarID = 3;
            car.Name = "car3";
            car.Model = "model3";
            car.Color = "color3";
            car.Year = "year3";
            car.Active = true;
            cars.Add(car);
        }

        public int insert(Car car)
        {
            int result = 0;
            int before = cars.Count;
            cars.Add(car);
            int after = cars.Count;
            result = after - before;
            return result;

        }

        public List<Car> selectCars()
        {
            return cars;
        }

        public int update(Car car)
        {
            int result = 0;
            foreach (Car _car in cars)
            {               
                if (_car.CarID == car.CarID)
                {
                    _car.Name = car.Name;
                    _car.Model = car.Model;
                    _car.Color = car.Color;
                    _car.Year = car.Year;
                    _car.Active = car.Active;
                    result = 1;
                    break;
                }
            }
            return result;
        }
    }
}
