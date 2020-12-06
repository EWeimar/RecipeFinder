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
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            //string username = txtUsername.Text.ToLower();
            //string password = txtPassword.Password;

            //if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            //{
            //    MessageBox.Show("Please fill in both username and password");
            //}
            //if (userService.ValidLogin(username, password))
            //{
            //    AdminWindow admin = new AdminWindow();
            //    admin.Show();
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Username and/or password is incorrect");
            //}

            //For Test Purpose

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
