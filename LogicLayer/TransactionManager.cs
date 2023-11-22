using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayerInterface;
using DataObject;
using DataAccessInterface;
using DataAccessLayer;

namespace LogicLayer
{
    public class TransactionManager : TransactionManagerInterface
    {
        private TransactionAccessorInterface _transactionAccessor;
        public TransactionManager()
        {
            _transactionAccessor = new TransactionAccessor();
        }

        public int addTransaction(Trans transaction)
        {
            int result = 0;
            result = _transactionAccessor.insert(transaction);
            return result;
        }

        public List<Trans> getTransactionByCustomerId(int customerID)
        {
            List<Trans> transactions = new List<Trans>();
            transactions = _transactionAccessor.selectTransactionByCustomerId(customerID);
            return transactions;
        }
    }
}
