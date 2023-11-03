using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInerface
{
    public interface EmployeeMangerInterface
    {
        List<Employee> GetAllEmployees();
        List<string> getEmployeeRoles(int isVerifyUser);
        public   int verifyUser(string userName, string password);
    }
}
