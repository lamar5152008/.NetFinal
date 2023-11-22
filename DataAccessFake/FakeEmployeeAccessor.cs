using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterface;
using DataObject;

namespace DataAccessFake
{
    public class FakeEmployeeAccessor : EmployeeAccessorInterface
    {
        private List<EmployeeVM> _employees;
        public FakeEmployeeAccessor() {
            _employees = new List<EmployeeVM>();
            addSampleData();
        }

        private void addSampleData()
        {
            List<String> Roles = new List<String>();
            Roles.Add("Admin");
            Roles.Add("Manager");
            Roles.Add("Resption");
            EmployeeVM employeeVM = new EmployeeVM();
            employeeVM.EmployeeID = 1;
            employeeVM.GivenName = "Test1";
            employeeVM.FamilyName = "Fam1";
            employeeVM.Phone = "123456789";
            employeeVM.Email = "email1@Email.com";
            employeeVM.Password = "newuser";
            employeeVM.Active = true;
            employeeVM.Roles = new List<string>();
            employeeVM.Roles.Add(Roles.ElementAt(0));

            employeeVM = new EmployeeVM();
            employeeVM.EmployeeID = 2;
            employeeVM.GivenName = "Test2";
            employeeVM.FamilyName = "Fam2";
            employeeVM.Phone = "123456783";
            employeeVM.Email = "email2@Email.com";
            employeeVM.Password = "newuser";
            employeeVM.Active = true;
            employeeVM.Roles = new List<string>();
            employeeVM.Roles.Add(Roles.ElementAt(1));

            employeeVM = new EmployeeVM();
            employeeVM.EmployeeID = 3;
            employeeVM.GivenName = "Test3";
            employeeVM.FamilyName = "Fam3";
            employeeVM.Phone = "123456783";
            employeeVM.Email = "email3@Email.com";
            employeeVM.Password = "newuser";
            employeeVM.Active = true;
            employeeVM.Roles = new List<string>();
            employeeVM.Roles.Add(Roles.ElementAt(2));


        }

        public int deleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public List<string> getEmployeeRoles(int employeeID)
        {
            throw new NotImplementedException();
        }

        public int insertNewEmployee(EmployeeVM employee)
        {
            int result = _employees.Count;
           _employees.Add(employee);
            return _employees.Count - result;
        }

        public List<Employee> selectAllEmployees()
        {
            throw new NotImplementedException();
        }

        public int updateEmployee(EmployeeVM employee)
        {
            throw new NotImplementedException();
        }

        public int verfiyUser(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
