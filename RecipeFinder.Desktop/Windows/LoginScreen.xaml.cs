using RecipeFinder.BusinessLayer.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace RecipeFinder.Desktop
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        //var client = new RestClient();
        //private UserService userService = new UserService();
        public LoginScreen()
        {
            InitializeComponent();

            txtUsername.Text = "test";
            txtPassword.Password = "123";
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.ToLower() == "test" && @txtPassword.Password == "123")
            {
                AdminWindow admin = new AdminWindow();
                admin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect");
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (txtUsername.Text.ToLower() == "test" && @txtPassword.Password == "123")
                {
                    AdminWindow admin = new AdminWindow();
                    admin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is incorrect");
                }
            }
        }
    }
}
