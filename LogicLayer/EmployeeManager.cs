using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterface;
using DataAccessLayer;
using DataAccessInterface;
using System.Security.Cryptography;
using DataObject;

namespace LogicLayer
{
    public class EmployeeManager : EmployeeManagerInterface
    {
        private EmployeeAccessorInterface _employeeAccessor;
        private EmployeeVM _employeeVM;
        private List<EmployeeVM> _employees;

        public EmployeeManager()
        {
            _employeeAccessor = new EmployeeAccessor();
            _employeeVM = new EmployeeVM();
            _employees = new List<EmployeeVM>();
        }

        public EmployeeManager(EmployeeAccessorInterface employeeAccessor)
        {
            _employeeAccessor = employeeAccessor;
            _employeeVM = new EmployeeVM();
            _employees = new List<EmployeeVM>();
        }

        public int addNewEmployee(EmployeeVM employee)
        {
            int employeeId = 0;
            try
            {
                employeeId = _employeeAccessor.insertNewEmployee(employee);
            }
            catch (Exception)
            {

                throw;
            }
            return employeeId;
        }

        public int deleteEmployee(Employee employee)
        {
            int result = 0;
            result = _employeeAccessor.deleteEmployee(employee);
            return result;
        }

        public int EditEmployee(EmployeeVM employee)
        {
            int result = 0;
            employee.Password = hashSHA256(employee.Password);
            result = _employeeAccessor.updateEmployee(employee);
            return result;
        }

        public List<EmployeeVM> GetAllEmployees()
        {
            _employees = _employeeAccessor.selectAllEmployees();
            return _employees; 
        }

        public List<string> getEmployeeRoles(int isVerifyUser)
        {
            List<string> roles = new List<string>();
            roles = _employeeAccessor.getEmployeeRoles(isVerifyUser);
            return roles;
        }

        public int verifyUser(string userName, string password)
        {
            int result = 0;
            string passwordHash = hashSHA256(password);
            result = _employeeAccessor.verfiyUser(userName, passwordHash);
            if (result!=0)
            {
              _employeeVM.EmployeeID = result;
                _employeeVM.Roles = _employeeAccessor.getEmployeeRoles(_employeeVM.EmployeeID);

            }
            return result;
        }

        private string hashSHA256(string source)
        {
            // defult password is newuser
            string result = "";
            byte[] data;
            using (SHA256 sha256sha = SHA256.Create())
            {
                data = sha256sha.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            var s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString();
            return result;
        }
        }
}
