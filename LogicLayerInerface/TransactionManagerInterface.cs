using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObject;

namespace LogicLayerInerface
{
    public interface TransactionManagerInterface
    {
        int addTransaction(Transaction transaction);
    }
}
