using DataObject;
using DataAccessFake;
using LogicLayer;
using LogicLayerInterface;
using DataAccessInterface;
using System.Transactions;
using System.Data.Common;
namespace LogicLayerTest
{
    public class TransactionManagerTest
    {
        private TransactionManagerInterface transactionManager;
        private TransactionAccessorInterface transactionAccessor;
        private Trans transaction;
        [SetUp]
        public void Setup()
        {
            transactionAccessor = new FakeTransactionAccessor();
            transactionManager = new LogicLayer.TransactionManager(transactionAccessor);
            transaction = new Trans();
        }

        [Test]
        public void TestInsertTransaction()
        {
            //1- initial
            transaction.ID = 4;
            transaction.CustomerId = 4;
            transaction.CarId = 4;
            transaction.Price = "4";
            transaction.Date = "4";
            int result = 1;
            int actual = transactionManager.addTransaction(transaction);
            Assert.That(actual, Is.EqualTo(result));
        }
        [Test]
        public void TestSelectTransactionByCustomerId()
        {
            //1- initial
            int customerId = 1;
            int result = 1;
            int actual = transactionManager.getTransactionByCustomerId(customerId).Count;
            Assert.That(actual, Is.EqualTo(result));
        }
    }
}