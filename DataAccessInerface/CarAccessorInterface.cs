using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;

namespace DataAccessInterface
{
    public interface CarAccessorInterface
    {
        public int insert(Car car);
        public List<Car> selectCars();
    }
}
