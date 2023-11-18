using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;

namespace DataAccessInterface
{
    public interface CustomerAccessorInterface
    {
        public int deleteCustomer(Customer customer);
        public int insertCustomer(Customer customer);
        public List<Customer> selectCustomers();
        public int updateCustomer(Customer customer);
    }
}
