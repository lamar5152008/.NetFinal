using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface EmployeeAccessorInterface
    {
        public int deleteEmployee(Employee employee);
        List<string> getEmployeeRoles(int employeeID);
        int insertNewEmployee(EmployeeVM employee);
        List<EmployeeVM> selectAllEmployees();
        int updateEmployee(EmployeeVM employee);
        int verfiyUser(string userName, string password);
    }
}
