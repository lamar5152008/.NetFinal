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
        int deleteEmployee(Employee employee);
        List<string> getEmployeeRoles(int employeeID);
        int insertNewEmployee(EmployeeVM employee);
        List<Employee> selectAllEmployees();
        int updateEmployee(EmployeeVM employee);
        int verfiyUser(string userName, string password);
    }
}
