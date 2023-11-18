using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Transaction
    {
        public int ID {  get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public string? Price { get; set; }
        public string? Date { get; set; }
    }
}
