using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;

namespace LogicLayerInterface
{
    public interface CustomerManagerInterface
    {
        public List<Customer> getAllCustomers();
    }
}
