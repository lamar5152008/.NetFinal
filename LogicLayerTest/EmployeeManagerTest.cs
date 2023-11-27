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
        [Test]
        public void TestDeleteEmployee()
        {
            //1- initial
            _employee.EmployeeID = 3;
            _employee.GivenName = "Abc";
            _employee.FamilyName = "DDD";
            _employee.Phone = "123456789";
            _employee.Email = "ABC@Email.com";
            _employee.Password = "newuser";
            _employee.Active = true;
            int result = 1;
            int actual = _EmployeeManager.deleteEmployee(_employee);
            Assert.That(actual, Is.EqualTo(result));
        }

        [Test]
        public void TestEditEmployee()
        {
            //1- initial
            _employee.EmployeeID = 3;
            _employee.GivenName = "Abd";
            _employee.FamilyName = "DDD";
            _employee.Phone = "123456789";
            _employee.Email = "ABC@Email.com";
            _employee.Password = "newuser";
            _employee.Active = true;
            int result = 1;
            int actual = _EmployeeManager.EditEmployee(_employee);
            Assert.That(actual, Is.EqualTo(result));
        }
        [Test]
        public void TestGetAllEmployees()
        {
            //1- initial
            int result = 3;
            int actual = _EmployeeManager.GetAllEmployees().Count;
            Assert.That(actual, Is.EqualTo(result));
        }

        [Test]
        public void TestGetEmployeeRoles()
        {
            //1- initial
            int employeeId = 3;
            int result = 1;
            int actual = _EmployeeManager.getEmployeeRoles(employeeId).Count;
            Assert.That(actual, Is.EqualTo(result));
        }
        [Test]
        public void TestVerifyUser()
        {
            //1- initial
            string username= "email1@Email.com";
            string password = "newuser";
            int result = 0;
            int actual = _EmployeeManager.verifyUser(username,password);
            Assert.That(actual, Is.GreaterThan(result));
        }
    }
}