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
        public int addCustomer(Customer customer);
        public int deleteCustomer(Customer customer);
        public int edit(Customer customer);
        public List<Customer> getAllCustomers();
    }
}
