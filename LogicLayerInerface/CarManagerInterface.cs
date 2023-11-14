using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterface
{
    public interface CarManagerInterface
    {
        public int add(Car car);
        public int edit(Car car);
        public List<Car> getAllCars();
    }
}
