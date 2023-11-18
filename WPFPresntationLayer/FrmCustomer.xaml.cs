using LogicLayer;
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
using LogicLayerInterface;
namespace WPFPresntationLayer
{
    /// <summary>
    /// Interaction logic for FrmCustomer.xaml
    /// </summary>
    public partial class FrmCustomer : Window
    {
        private Customer customer = null;
        private CustomerManagerInterface customerManager = new CustomerManager();
        public FrmCustomer()
        {
            InitializeComponent();
            customer = new Customer();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!validForm())
            {
                return;
            }
            int result = 0;
            customer.FirstName = txtFirstName.Text;
            customer.LastName = txtLastname.Text;
            customer.Email = txtEmail.Text;
            customer.Phone = txtPhone.Text;
            result = customerManager.addCustomer(customer);
            if (result == 0) { lblGeneralMessage.Content = "did not goes right!"; return; }
            lblGeneralMessage.Content = "user added correctly";
        }

        private bool validForm()
        {
            if (txtFirstName.Text.Length == 0) {
                lblGeneralMessage.Content = "First Name require";
                return false;
            }
            if (txtLastname.Text.Length == 0)
            {
                lblGeneralMessage.Content = "Last Name require";
                return false;
            }
            if (txtPhone.Text.Length == 0)
            {
                lblGeneralMessage.Content = "Phone require";
                return false;
            }
            if (txtEmail.Text.Length == 0)
            {
                lblGeneralMessage.Content = "Email require";
                return false;
            }
            lblGeneralMessage.Content = string.Empty;
            return true;
        }
    }
}
