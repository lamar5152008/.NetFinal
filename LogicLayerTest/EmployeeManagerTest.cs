using DataObject;
using DataAccessFake;
using LogicLayer;
using LogicLayerInterface;
using DataAccessInterface;
namespace LogicLayerTest
{
    public class EmployeeManagerTest
    {
        private EmployeeManagerInterface _EmployeeManager;
        private EmployeeAccessorInterface _EmployeeAccessor;
        private EmployeeVM _employee;
        [SetUp]
        public void Setup()
        {
            _EmployeeAccessor = new FakeEmployeeAccessor();
            _EmployeeManager = new EmployeeManager(_EmployeeAccessor);
            _employee = new EmployeeVM();
        }

        [Test]
        public void TestAddEmployee()
        {
            //1- initial
            _employee.EmployeeID = 10;
            _employee.GivenName = "Abc";
            _employee.FamilyName = "DDD";
            _employee.Phone = "123456789";
            _employee.Email = "ABC@Email.com";
            _employee.Password = "newuser";
            _employee.Active = true;
            List<string> list = new List<string>();
            list.Add("Admin");
            _employee.Roles = list;
            int result = 1;
            int actual = _EmployeeManager.addNewEmployee(_employee);
            Assert.That(actual, Is.EqualTo(result));
        }
        //[Test]
        //public void TestDeleteCustomer()
        //{
        //    //1- initial
        //    _employee.CustomerID = 3;
        //    _employee.FirstName = "test3";
        //    _employee.LastName = "last3";
        //    _employee.Phone = "Phone3";
        //    _employee.Email = "Email3";
        //    int result = 1;
        //    int actual = _customerManager.deleteCustomer(_employee);
        //    Assert.That(actual, Is.EqualTo(result));
        //}

        //[Test]
        //public void TestEditCustomer()
        //{
        //    //1- initial
        //    Customer customer = new Customer();
        //    customer.CustomerID = 1;
        //    customer.FirstName = "customer1";
        //    customer.LastName = "lastLast";
        //    customer.Phone = "999999999";
        //    customer.Email = "email@Email.com";
        //    int result = 1;
        //    int actual = _customerManager.edit(customer);
        //    Assert.That(actual, Is.EqualTo(result));
        //}
        //[Test]
        //public void TestGetAllCustomers()
        //{
        //    //1- initial
        //    int result = 3;
        //    int actual = _customerManager.getAllCustomers().Count;
        //    Assert.That(actual, Is.EqualTo(result));
        //}
    }
}