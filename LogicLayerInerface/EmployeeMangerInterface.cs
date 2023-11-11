﻿using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerInterface
{
    public interface EmployeeMangerInterface
    {
        public int addNewEmployee(EmployeeVM employee);
        int deleteEmployee(Employee employee);
        int EditEmployee(EmployeeVM employee);
        public List<Employee> GetAllEmployees();
        public List<string> getEmployeeRoles(int isVerifyUser);
        public int verifyUser(string userName, string password);
    }
}
