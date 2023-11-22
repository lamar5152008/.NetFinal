using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataObject;
using LogicLayer;
using LogicLayerInterface;
using LogicLayerInterface;

namespace WPFPresntationLayer
{
    /// <summary>
    /// Interaction logic for FRMTransactions.xaml
    /// </summary>
    public partial class FRMTransactions : Window
    {
        private Customer customer;
        private CarManagerInterface carManager;
        private Car car;
        private List<Car> cars;
        private TransactionManagerInterface transactionManager;
        private Trans transaction = null;
        private List<Trans> transactions = null;
        public FRMTransactions(Customer customer)
        {
            InitializeComponent();
            this.customer = customer;
            carManager = new CarManager();
            transactionManager = new TransactionManager();
            cars = new List<Car>();
            car = new Car();
            transaction = new Trans();
            transactions = new List<Trans>();
            insertCustomerData();
            retrieveCarData();
            showCustomerTransactions();
            txtDate.Text = DateTime.Now.ToString();
        }

        private void showCustomerTransactions()
        {
            //
            transactions = transactionManager.getTransactionByCustomerId(customer.CustomerID);
            dataGridTransactions.ItemsSource = transactions;
        }

        private void retrieveCarData()
        {
            
            cars = carManager.getAllCars();
            List<string> carsNames = new List<string>();
            foreach (Car car1 in cars)
            {
                carsNames.Add(car1.Name);
            }
            comboCar.ItemsSource = carsNames;
        }

        private void insertCustomerData()
        {
            lblName.Content = customer.FirstName + " " + customer.LastName;
            lblPhone.Content = customer.Phone;
            lblEmail.Content = customer.Email;
        }

        private void comboCar_DropDownClosed(object sender, EventArgs e)
        {
            if (comboCar.SelectedItem != null) { 
                string carName = comboCar.SelectedItem.ToString();
                foreach (Car car1 in cars) {
                    if (car1.Name == carName)
                    {
                        lblCarModel.Content = car1.Model;
                        lblCarColor.Content = car1.Color;
                        lblCarYear.Content = car1.Year;
                        break;
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!validData())
            {
                return;
            }
            int result = 0;
            transaction.CustomerId = customer.CustomerID;
            transaction.CarId = getCarId(comboCar.SelectedItem.ToString());
            if (transaction.CarId <= 0)
            {
                lblTransactionNote.Content = "Please select a car!";
                return;
            }
            lblTransactionNote.Content = "";
            transaction.Price = txtPrice.Text;
            transaction.Date = txtDate.Text;
            result = transactionManager.addTransaction(transaction);
            if (result == 0) 
            {
                lblTransactionNote.Content = "transaction did not done!";
                return; 
            }
            lblTransactionNote.Content = "Transaction Added Correctly";
            showCustomerTransactions();


        }


        private int getCarId(string selectedItem)
        {
            foreach (Car car in cars)
            {
                if (car.Name.Equals(selectedItem))
                {
                    return car.CarID;
                }
            }
            return -1;
        }

        private bool validData()
        {
            if (comboCar.SelectedItem == null)
            {
                lblTransactionNote.Content = "please choose a car";
                return false;
            }
            if (txtPrice.Text.Length == 0) {
                lblTransactionNote.Content = "price require";
                return false;
            }
            if (txtDate.Text.Length == 0)
            {
                lblTransactionNote.Content = "date require";
                return false;
            }
            lblTransactionNote.Content = "";
            return true;
        }
    }
}
