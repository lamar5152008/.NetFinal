using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayer;
using LogicLayerInterface;
using DataObject;
using System.Security.AccessControl;

namespace WPFPresntationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 11/8/2023
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeManagerInterface _employeeManger;
        private List<Employee> _employees;
        private Employee oldEmployee = new Employee();
        private CarManagerInterface carManager;
        private bool editCar = false;
        private Car oldCar = new Car();
        private CustomerManagerInterface customerManager;
        private Customer customer;
        public MainWindow()
        {
            InitializeComponent();
            _employeeManger = new EmployeeManger();
            tcAdmin.Visibility = Visibility.Hidden;
            tcManager.Visibility = Visibility.Hidden;
            tcResption.Visibility = Visibility.Hidden;
            _employees = new List<Employee>();
            carManager = new CarManager();
            customerManager = new CustomerManager();
            customer = new Customer();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userName = txt_username.Text;
            string password = txt_password.Password.ToString();

            Boolean isUserNameValid = validatUserName(userName);
            Boolean isPasswordValid = validatPassword(password);
            if (isUserNameValid && isPasswordValid)
            {
                lblLoginMessages.Content = "user name and password valid";
                int isVerifyUser = _employeeManger.verifyUser(userName, password);
                if (isVerifyUser != 0)
                {
                    List<string> roles = new List<string>();
                    roles = _employeeManger.getEmployeeRoles(isVerifyUser);
                    foreach (string role in roles)
                    {
                        lblLoginMessages.Content += role;
                        if (role == "Admin")
                        {
                            tcAdmin.Visibility = Visibility.Visible;
                            tcManager.Visibility = Visibility.Hidden;
                            tcResption.Visibility = Visibility.Hidden;
                            _employees = _employeeManger.GetAllEmployees();
                            dgAllEmployee.ItemsSource = _employees;
                        }
                        if (role == "Manager")
                        {
                            tcAdmin.Visibility = Visibility.Hidden;
                            tcManager.Visibility = Visibility.Visible;
                            tcResption.Visibility = Visibility.Hidden;
                            List<Car> cars = new List<Car>();
                            cars = carManager.getAllCars();
                            dgCars.ItemsSource = cars;
                        }
                        if (role == "Reciption")
                        {
                            tcAdmin.Visibility = Visibility.Hidden;
                            tcManager.Visibility = Visibility.Hidden;
                            tcResption.Visibility = Visibility.Visible;
                            List<Customer> customers = new List<Customer>();
                            customers = customerManager.getAllCustomers();
                            dataGridReciption.ItemsSource = customers;
                        }
                    }
                }
                else
                {
                    lblLoginMessages.Content = "user name and password valid but not verify";
                }
            }
            else
            {
                lblLoginMessages.Content = "user name and password not valid";

            }
        }

        private bool validatPassword(string password)
        {
            Boolean result = true;
            if (String.IsNullOrEmpty(password))
            {
                result = false;
            }
            return result;
        }

        private Boolean validatUserName(string userName)
        {
            Boolean result = false;
            if (userName != null)
            {
                result = true;
            }
            return result;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool isFormValid = validateForm();
            if (!isFormValid) { return; }
            EmployeeVM employee = new EmployeeVM();
            employee.GivenName = txtGivenName.Text;
            employee.FamilyName = txtFamilyName.Text;
            employee.Email = txtEmail.Text;
            employee.Phone = txtPhone.Text;
            employee.Active = cbActive.IsChecked == true;
            employee.Password = txtPassword.Text;
            employee.Roles = new List<string>();
            employee.Roles.Add("Reciption");
            int employeeId = 0;
            if (btnSubmit.Content.ToString() == "New")
            {
                employeeId = _employeeManger.addNewEmployee(employee);

                if (employeeId == 0)
                {
                    lblAdminMessages.Content = "new Employee did not save";
                    return;
                }
                lblAdminMessages.Content = "new Employee added correctly";

            }
            else
            {
                employee.EmployeeID = oldEmployee.EmployeeID;
                int result = _employeeManger.EditEmployee(employee);
                if (result != 0)
                {
                    lblAdminMessages.Content = "update done correctly";
                    btnSubmit.Content = "New";
                }
                else
                {
                    lblAdminMessages.Content = "there is and error, try again";
                    return;
                }
            }

            clearFormData();
            _employees = new List<Employee>();
            _employees = _employeeManger.GetAllEmployees();
            dgAllEmployee.ItemsSource = _employees;

        }

        private void clearFormData()
        {
            txtGivenName.Text = "";
            txtFamilyName.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtPassword.Text = "";
            cbActive.IsChecked = false;
        }

        private bool validateForm()
        {
            if (txtGivenName.Text.Length == 0)
            {
                lblAdminMessages.Content = "Given Name Require";
                return false;
            }
            if (txtFamilyName.Text.Length == 0)
            {
                lblAdminMessages.Content = "Family Name Require";
                return false;
            }
            if (txtPhone.Text.Length == 0)
            {
                lblAdminMessages.Content = "Phone Require";
                return false;
            }
            if (txtEmail.Text.Length == 0)
            {
                lblAdminMessages.Content = "Email Require";
                return false;
            }
            if (txtPassword.Text.Length == 0)
            {
                lblAdminMessages.Content = "Password Require";
                return false;
            }
            lblAdminMessages.Content = "";
            return true;
        }

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = (Employee)dgAllEmployee.SelectedItem;
            if (employee != null)
            {
                int result = _employeeManger.deleteEmployee(employee);
                if (result == 0)
                {
                    lblAdminMessages.Content = "Employee did not delete yet!";
                    return;
                }
                lblAdminMessages.Content = "Employee deleted correctly";
                _employees = new List<Employee>();
                _employees = _employeeManger.GetAllEmployees();
                dgAllEmployee.ItemsSource = _employees;
            }
            else
            {
                lblAdminMessages.Content = "Please select an oldEmployee";
                return;
            }




        }

        private void dgAllEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            oldEmployee = (Employee)dgAllEmployee.SelectedItem;
            txtGivenName.Text = oldEmployee.GivenName;
            txtFamilyName.Text = oldEmployee.FamilyName;
            txtEmail.Text = oldEmployee.Email;
            txtPassword.Text = oldEmployee.Password;
            txtPhone.Text = oldEmployee.Phone;
            cbActive.IsChecked = oldEmployee.Active;
            btnSubmit.Content = "Update";
        }

        private void btnSubmitCar_Click(object sender, RoutedEventArgs e)
        {
            if (!validateCarForm())
            {
                return;
            }
            Car car = new Car();
            car.Name = txtNameCar.Text;
            car.Model = txtModelCar.Text;
            car.Color = txtColorCar.Text;
            car.Year = txtYearCar.Text;
            car.Active = cbActiveCar.IsChecked == true;
            int result = 0;
            if (editCar)
            {
                car.CarID = oldCar.CarID;
                result = carManager.edit(car);
            }
            else
            {
                result = carManager.add(car);
                editCar = false;
            }
            if (result == 0)
            {
                lblCarFromError.Content = "there is an error, please contact admin";
            }
            else
            {
                lblCarFromError.Foreground = Brushes.Black;
                lblCarFromError.Content = "car data added correctly";
                clearCarForm();
                List<Car> cars = new List<Car>();
                cars = carManager.getAllCars();
                dgCars.ItemsSource = cars;
            }
        }

        private void clearCarForm()
        {
            txtNameCar.Text = "";
            txtModelCar.Text = "";
            txtColorCar.Text = "";
            txtYearCar.Text = "";
            cbActive.IsChecked = true;
        }

        private bool validateCarForm()
        {
            if (txtNameCar.Text.Length == 0)
            {
                lblCarFromError.Content = "Name of car is require";
                txtNameCar.Focus();
                return false;
            }
            if (txtModelCar.Text.Length == 0)
            {
                lblCarFromError.Content = "Model of car is require";
                txtModelCar.Focus();
                return false;
            }
            if (txtColorCar.Text.Length == 0)
            {
                lblCarFromError.Content = "Color of car is require";
                txtColorCar.Focus();
                return false;
            }
            if (txtYearCar.Text.Length == 0)
            {
                lblCarFromError.Content = "Year of car is require";
                txtYearCar.Focus();
                return false;
            }
            lblCarFromError.Content = "";
            btnSubmitCar.Focus();
            return true;
        }

        private void dgCars_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            oldCar = (Car) dgCars.SelectedItem;
            txtNameCar.Text = oldCar.Name;
            txtModelCar.Text = oldCar.Model;
            txtColorCar.Text = oldCar.Color;
            txtYearCar.Text = oldCar.Year;
            cbActive.IsChecked = oldCar.Active;
            editCar = true;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            FrmCustomer frmCustomer = new FrmCustomer();
            frmCustomer.ShowDialog();
            List<Customer> customers = new List<Customer>();
            customers = customerManager.getAllCustomers();
            dataGridReciption.ItemsSource = customers;
        }

        private void dataGridReciption_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridReciption.SelectedItem == null)
            {
                lblResptionGeneralNote.Content = "please chose a customer first";
                return;
            }
            Customer customer = new Customer();
            customer = (Customer) dataGridReciption.SelectedItem;
            FrmCustomer customerForm = new FrmCustomer(customer);
            customerForm.ShowDialog();
            List<Customer> customers = new List<Customer>();
            customers = customerManager.getAllCustomers();
            dataGridReciption.ItemsSource = customers;
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridReciption.SelectedItem == null)
            {
                lblResptionGeneralNote.Content = "Please choose arow";
                return;
            }
            Customer customer = new Customer();
            customer = (Customer ) dataGridReciption.SelectedItem;
            int result = 0;
            result = customerManager.deleteCustomer(customer);
            if (result ==0)
            {
                lblResptionGeneralNote.Content = "customer did not deleted!";
                return;
            }
            lblResptionGeneralNote.Content = "";
            List<Customer> customers = new List<Customer>();
            customers = customerManager.getAllCustomers();
            dataGridReciption.ItemsSource = customers;
        }

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridReciption.SelectedItem == null)
            {
                lblResptionGeneralNote.Content = "Please choose a customer first";
                return;
            }
            Customer customer = new Customer();
            customer = (Customer ) dataGridReciption.SelectedItem;
            FRMTransactions fRMTransactions = new FRMTransactions(customer);
            fRMTransactions.ShowDialog();
        }
    }
}
