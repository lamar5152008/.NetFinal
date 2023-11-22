using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;

namespace LogicLayerInterface
{
    public interface TransactionManagerInterface
    {
        int addTransaction(Trans transaction);
        public List<Trans> getTransactionByCustomerId(int customerID);
    }
}
