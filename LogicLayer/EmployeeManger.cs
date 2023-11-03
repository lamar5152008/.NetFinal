using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInerface;
using DataAccessLayer;
using DataAccessInerface;
using System.Security.Cryptography;
using DataObject;

namespace LogicLayer
{
    public class EmployeeManger : EmployeeMangerInterface
    {
        private EmployeeAccessorInterface _employeeAccessor = new EmployeeAccessor();
        private EmployeeVM _employeeVM = new EmployeeVM();
        private List<Employee> _employees = new List<Employee>();

        public List<Employee> GetAllEmployees()
        {
            _employees = _employeeAccessor.selectAllEmployees();
            return _employees; 
        }

        public List<string> getEmployeeRoles(int isVerifyUser)
        {
            return _employeeVM.Roles;
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
