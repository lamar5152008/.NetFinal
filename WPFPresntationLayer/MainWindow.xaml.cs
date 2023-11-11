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
using LogicLayerInerface;
using DataObject;

namespace WPFPresntationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 11/8/2023
    /// </summary>
    public partial class MainWindow : Window
    {
        private EmployeeMangerInterface _employeeManger;
        private List<Employee> _employees;
        Employee oldEmployee = new Employee();
        public MainWindow()
        {
            InitializeComponent();
            _employeeManger = new EmployeeManger();
            tcAdmin.Visibility = Visibility.Hidden;
            tcManager.Visibility = Visibility.Hidden;
            tcResption.Visibility = Visibility.Hidden;
            _employees = new List<Employee>();
            _employees = _employeeManger.GetAllEmployees();
            dgAllEmployee.ItemsSource = _employees;
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
                        }
                        if (role == "Manager")
                        {
                            tcAdmin.Visibility = Visibility.Hidden;
                            tcManager.Visibility = Visibility.Visible;
                            tcResption.Visibility = Visibility.Hidden;
                        }
                        if (role == "Reciption")
                        {
                            tcAdmin.Visibility = Visibility.Hidden;
                            tcManager.Visibility = Visibility.Hidden;
                            tcResption.Visibility = Visibility.Visible;
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
    }
}
