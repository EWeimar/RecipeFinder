﻿using RecipeFinder.Desktop.ApiHelpers;
using RecipeFinder.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        RecipeCaller recipeCaller = new RecipeCaller(ConfigurationManager.AppSettings["RecipeFinderApiBaseUrl"]);
        public AdminWindow()
        {
            InitializeComponent();
            
            FillDataGrid();
        }

        private void InsertRecipes()
        {
            grdRecipes.ItemsSource = recipeCaller.GetAll().recipes;
        }

        private void FillDataGrid()
        {
            InsertRecipes();
        }

        private List<RecipeModel> GetAllRecipes()
        {
            RecipeCaller rc = new RecipeCaller(ConfigurationManager.AppSettings["RecipeFinderApiBaseUrl"]);
            return rc.GetAll().recipes;
        }

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature is not yet implemented \nTry another one");
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen Login = new LoginScreen();
            Login.Show();
            this.Close();
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature is not yet implemented \nTry another one");
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature is not yet implemented \nTry another one");
        }

        private void btnCreateRecipe_Click(object sender, RoutedEventArgs e)
        {
            CreateRecipe create = new CreateRecipe();
            create.Show();
            this.Close();
        }

        private void btnUpdateRecipe_Click(object sender, RoutedEventArgs e)
        {

            if (grdRecipes.SelectedItem == null)
            {
                MessageBox.Show("Oops! Please Select a Recipe");
            }
            else
            {
                RecipeModel selectedRecipe = grdRecipes.SelectedItem as RecipeModel;

                UpdateRecipe update = new UpdateRecipe(selectedRecipe.Id);
                update.Show();
                this.Close();
            }      
        }

        private void btnDeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature is not yet implemented \nTry another one");
        }

        private void grdUsers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

        }
    }
}
