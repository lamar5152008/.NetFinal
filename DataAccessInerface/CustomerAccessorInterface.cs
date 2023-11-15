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
        public int insertCustomer(Customer customer);
        List<Customer> selectCustomers();
    }
}
