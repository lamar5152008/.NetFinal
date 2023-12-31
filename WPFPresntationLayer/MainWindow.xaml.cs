﻿using System;
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
    /// </summary>
    public partial class MainWindow : Window
    {
       private EmployeeMangerInterface _employeeManger;
        private List<Employee> _employees; 
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
            if (isUserNameValid && isPasswordValid )
            {
                lblLoginMessages.Content = "user name and password valid";
                int isVerifyUser = _employeeManger.verifyUser(userName, password);
                if (isVerifyUser!=0 )
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
        }

        private bool validateForm()
        {
            if (txtGivenName.Text.Length == 0)
            {
                lblAdminMessages.Content = "Given Name Require";
                return false;
            }
            if (txtFamilyName.Text.Length == 0) {
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
    }
}
