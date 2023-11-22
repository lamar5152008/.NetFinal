using DataObject;
using DataAccessFake;
using LogicLayer;
using LogicLayerInterface;
using DataAccessInterface;
namespace LogicLayerTest
{
    public class CustomerManagerTest
    {
        private CustomerManagerInterface _customerManager;
        private CustomerAccessorInterface _CustomerAccessor;
        private Customer _customer;
        [SetUp]
        public void Setup()
        {
            _CustomerAccessor = new FakeCustomerAccessor();
            _customerManager = new CustomerManager(_CustomerAccessor);
            _customer = new Customer();
        }

        [Test]
        public void TestInsertCustomerWithRealData()
        {
            //1- initial
            _customer.CustomerID = 10;
            _customer.FirstName = "Abc";
            _customer.LastName = "DDD";
            _customer.Phone = "123456789";
            _customer.Email = "ABC@Email.com";
            int result = 1;
            int actual = _customerManager.addCustomer(_customer);
            Assert.That(actual, Is.EqualTo(result));
        }
        [Test]
        public void TestDeleteCustomer()
        {
            //1- initial
            _customer.CustomerID = 3;
            _customer.FirstName = "test3";
            _customer.LastName = "last3";
            _customer.Phone = "Phone3";
            _customer.Email = "Email3";
            int result = 1;
            int actual = _customerManager.deleteCustomer(_customer);
            Assert.That(actual, Is.EqualTo(result));
        }

        [Test]
        public void TestEditCustomer()
        {
            //1- initial
            Customer customer = new Customer();
            customer.CustomerID = 1;
            customer.FirstName = "customer1";
            customer.LastName = "lastLast";
            customer.Phone = "999999999";
            customer.Email = "email@Email.com";
            int result = 1;
            int actual = _customerManager.edit(customer);
            Assert.That(actual, Is.EqualTo(result));
        }
        [Test]
        public void TestGetAllCustomers()
        {
            //1- initial
            int result = 3;
            int actual = _customerManager.getAllCustomers().Count;
            Assert.That(actual, Is.EqualTo(result));
        }
    }
}