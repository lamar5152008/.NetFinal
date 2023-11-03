using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInerface
{
    public interface EmployeeAccessorInterface
    {
        List<string> getEmployeeRoles(int employeeID);
        List<Employee> selectAllEmployees();
        int verfiyUser(string userName, string password);
    }
}
