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
            employeeVM.Password = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            employeeVM.Active = true;
            employeeVM.Roles = new List<string>();
            employeeVM.Roles.Add(Roles.ElementAt(0));
            _employees.Add(employeeVM);

            employeeVM = new EmployeeVM();
            employeeVM.EmployeeID = 2;
            employeeVM.GivenName = "Test2";
            employeeVM.FamilyName = "Fam2";
            employeeVM.Phone = "123456783";
            employeeVM.Email = "email2@Email.com";
            employeeVM.Password = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            employeeVM.Active = true;
            employeeVM.Roles = new List<string>();
            employeeVM.Roles.Add(Roles.ElementAt(1));
            _employees.Add(employeeVM);

            employeeVM = new EmployeeVM();
            employeeVM.EmployeeID = 3;
            employeeVM.GivenName = "Test3";
            employeeVM.FamilyName = "Fam3";
            employeeVM.Phone = "123456783";
            employeeVM.Email = "email3@Email.com";
            employeeVM.Password = "9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e";
            employeeVM.Active = true;
            employeeVM.Roles = new List<string>();
            employeeVM.Roles.Add(Roles.ElementAt(2));
            _employees.Add(employeeVM);

        }

        public int deleteEmployee(Employee employee)
        {
            int result = 0;
            EmployeeVM employeesVM = (EmployeeVM) employee;
            foreach (EmployeeVM emp in _employees)
            {
                if (emp.EmployeeID == employee.EmployeeID)
                {
                    _employees.Remove(emp);
                    result = 1;
                    break;
                }
            }
                return result;
        }

        public List<string> getEmployeeRoles(int employeeID)
        {
            List<string> roles = new List<string>();
            foreach (EmployeeVM emp in _employees)
            {
                if (emp.EmployeeID == employeeID)
                {
                    roles = emp.Roles;
                    break;
                }
            }
            return roles;
        }

        public int insertNewEmployee(EmployeeVM employee)
        {
            int result = _employees.Count;
           _employees.Add(employee);
            return _employees.Count - result;
        }

        public List<EmployeeVM> selectAllEmployees()
        {
            return _employees;
        }

        public int updateEmployee(EmployeeVM employee)
        {
            int result = 0;
            foreach (EmployeeVM emp in _employees)
            {
                if (emp.EmployeeID == employee.EmployeeID)
                {
                    emp.GivenName = employee.GivenName;
                    emp.FamilyName = employee.FamilyName;
                    emp.Phone = employee.Phone;
                    emp.Email = employee.Email;
                    emp.Password = employee.Password;
                    emp.Active = employee.Active;
                    result = 1;
                    break;
                }
            }
            return result;
        }

        public int verfiyUser(string userName, string password)
        {
            int result = 0;
            foreach (EmployeeVM employee in _employees)
            {
                if (employee.Email == userName && employee.Password == password)
                {
                    result = employee.EmployeeID;
                    break;
                }
            }
            return result;
        }

    }
}
