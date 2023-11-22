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
        private CustomerAccessorInterface customerAccessor;

        public CustomerManager()
        {
            customerAccessor = new CustomerAccessor();
        }

        public CustomerManager(CustomerAccessorInterface customerAccessor)
        {
            this.customerAccessor = customerAccessor;
        }

        public int addCustomer(Customer customer)
        {
            int result = 0;
            result = customerAccessor.insertCustomer(customer);
            return result;
        }

        public int deleteCustomer(Customer customer)
        {
            int result = 0;
            result = customerAccessor.deleteCustomer(customer);
            return result;
        }

        public int edit(Customer customer)
        {
            int result = 0;
            result = customerAccessor.updateCustomer(customer);
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
