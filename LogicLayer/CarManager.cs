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
        public List<Car> getAllCars()
        {
            List<Car> cars = new List<Car>();
            cars = carAccessor.selectCars();
            return cars;

        }
    }
}
