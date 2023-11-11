using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Car
    {
        public int CarID { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? Year { get; set; }
        public bool Active { get; set; }
    }
}
