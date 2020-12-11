using RecipeFinder.Desktop.ApiHelpers;
using RecipeFinder.Desktop.Models;
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
using System.Windows.Shapes;

namespace RecipeFinder.Desktop
{
    /// <summary>
    /// Interaction logic for CreateRecipe.xaml
    /// </summary>
    public partial class UpdateRecipe : Window
    {
        RecipeModel recipe = null;
        public UpdateRecipe(int RecipeId)
        {
            InitializeComponent();
            recipe = GetRecipeById(RecipeId);
        }

        public RecipeModel GetRecipeById(int RecipeId)
        {
            RecipeCaller rc = new RecipeCaller("https://localhost:44320/api");

            return rc.FindByCondition("Id", RecipeId);
        }

        private void lblIngredients_TouchEnter(object sender, TouchEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow admin = new AdminWindow();
            admin.Show();
            this.Close();
        }

        private void btnAddIngr_Click(object sender, RoutedEventArgs e)
        {

        }
    }    
}
