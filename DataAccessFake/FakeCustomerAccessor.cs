using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterface;
using DataObject;

namespace DataAccessFake
{
    public class FakeCustomerAccessor : CustomerAccessorInterface
    {
        private List<Customer> _customerList;
        public FakeCustomerAccessor() { 
            _customerList = new List<Customer>();
            createSampleData();
        }

        private void createSampleData()
        {
            Customer customer = new Customer();
            customer.CustomerID = 1;
            customer.FirstName = "Test1";
            customer.LastName = "last1";
            customer.Phone = "Phone1";
            customer.Email = "Email1";
            _customerList.Add(customer);
            customer = new Customer();
            customer.CustomerID = 2;
            customer.FirstName = "test2";
            customer.LastName = "last2";
            customer.Phone = "Phone2";
            customer.Email = "Email2";
            _customerList.Add(customer);
            customer = new Customer();
            customer.CustomerID = 3;
            customer.FirstName = "test3";
            customer.LastName = "last3";
            customer.Phone = "Phone3";
            customer.Email = "Email3";
            _customerList.Add(customer);
        }

        public int deleteCustomer(Customer customer)
        {
            int expected = _customerList.Count;
            foreach (Customer item in _customerList)
            {
                if (item.CustomerID == customer.CustomerID)
                {
                    _customerList.Remove(item);
                    break;
                }
            }
            return expected - _customerList.Count ;
        }

        public int insertCustomer(Customer customer)
        {
            int expected = _customerList.Count;
            _customerList.Add(customer);
            return _customerList.Count - expected;
        }

        public List<Customer> selectCustomers()
        {
            return _customerList;
        }

        public int updateCustomer(Customer customer)
        {
            int result = 0;
            foreach (Customer item in _customerList)
            {
                if (item.CustomerID == customer.CustomerID)
                {
                    item.FirstName = customer.FirstName;
                    item.LastName = customer.LastName;
                    item.Phone = customer.Phone;
                    item.Email = customer.Email;
                    result = 1;
                    break;
                }
            }
            return result;
        }
    }
}
