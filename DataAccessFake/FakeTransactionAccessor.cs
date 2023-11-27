using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterface;
using DataObject;

namespace DataAccessFake
{
    public class FakeTransactionAccessor : TransactionAccessorInterface
    {
        private Trans trans;
        private List<Trans> transactions;

        public FakeTransactionAccessor()
        {
            transactions = new List<Trans>();
            createSampleData();
        }

        private void createSampleData()
        {
            trans = new Trans();
            trans.ID = 1;
            trans.CustomerId = 1;
            trans.CarId = 1;
            trans.Price = "1";
            trans.Date = "1";
            transactions.Add(trans);

            trans = new Trans();
            trans.ID = 2;
            trans.CustomerId = 2;
            trans.CarId = 2;
            trans.Price = "2";
            trans.Date = "2";
            transactions.Add(trans);

            trans = new Trans();
            trans.ID = 3;
            trans.CustomerId = 3;
            trans.CarId = 3;
            trans.Price = "3";
            trans.Date = "3";
            transactions.Add(trans);
        }

        public int insert(Trans transaction)
        {
            int result = transactions.Count;
            transactions.Add(transaction);
            return transactions.Count - result;
        }

        public List<Trans> selectTransactionByCustomerId(int customerID)
        {
            List<Trans> transes = new List<Trans>();
            foreach (Trans trans in transactions)
            {
                if (trans.CustomerId == customerID)
                {
                    transes.Add(trans);
                }
            }
            return transes;
        }
    }
}
