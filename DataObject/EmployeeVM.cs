﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class EmployeeVM: Employee
    {
        public List<string> Roles {  get; set; }
    }
}
