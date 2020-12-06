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

namespace RecipeFinder.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            //FillDataGrids();
        }

        //private void FillDataGrids()
        //{
            //TODO
        //}

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen Login = new LoginScreen();
            Login.Show();
            this.Close();
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateRecipe_Click(object sender, RoutedEventArgs e)
        {
            CreateRecipe create = new CreateRecipe();
            create.Show();
            this.Close();
        }

        private void btnUpdateRecipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {

        }

        private void grdUsers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
    }
}
