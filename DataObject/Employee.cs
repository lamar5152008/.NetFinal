using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Employee
    {
        public int EmployeeID {  get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set;}
        public string Phone { get; set; }
        public string Email { get; set;}
        public string Password { get; set;}
        public bool Active { get; set;}
    }
}
