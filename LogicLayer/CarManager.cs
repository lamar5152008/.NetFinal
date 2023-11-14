using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;
using DataAccessLayer;
using DataAccessInterface;
using LogicLayerInterface;

namespace LogicLayer
{
    public class CarManager : CarManagerInterface
    {
        private CarAccessorInterface carAccessor = new CarAccessor();

        public int add(Car car)
        {
            int result = 0;
            result = carAccessor.insert(car);
            return result;
        }

        public int edit(Car car)
        {
            int result = 0;
            result = carAccessor.update(car);
            return result;
        }

        public List<Car> getAllCars()
        {
            List<Car> cars = new List<Car>();
            cars = carAccessor.selectCars();
            return cars;

        }
    }
}
