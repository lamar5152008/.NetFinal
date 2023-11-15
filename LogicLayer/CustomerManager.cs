using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterface;
using DataObject;
using DataAccessInterface;
using DataAccessLayer;

namespace LogicLayer
{
    public class CustomerManager : CustomerManagerInterface
    {
        private CustomerAccessorInterface customerAccessor = new CustomerAccessor();

        public int addCustomer(Customer customer)
        {
            int result = 0;
            result = customerAccessor.insertCustomer(customer);
            return result;
        }

        public List<Customer> getAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers = customerAccessor.selectCustomers();
            return customers;
        }
    }
}
