using RecipeFinder.Desktop.ApiHelpers;
using RecipeFinder.Desktop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        ObservableCollection<IngredientLineModel> lines;

        RecipeCaller rc;
        public UpdateRecipe(int RecipeId)
        {
            rc = new RecipeCaller("https://localhost:44320/api");
            InitializeComponent();
            recipe = GetRecipeById(RecipeId);
            SetData();

        }

        private void SetData()
        {
            txtTitle.Text = recipe.Title;
            txtInstructions.Text = recipe.Instruction;
            AddMeasureUnits();
            lines = new ObservableCollection<IngredientLineModel>(recipe.IngredientLines);
            grdIngredientLines.ItemsSource = lines;
        }

        private void AddMeasureUnits()
        {
            
            
            foreach (MeasureUnitModel measureUnit in rc.GetMeasureUnits())
            {
                cmbUnits.Items.Add(new { MeasureUnit = measureUnit.Name, MeasureUnitInt = measureUnit.Number});
            }

        }

        public RecipeModel GetRecipeById(int RecipeId)
        {
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

        private void AddIngredientLine()
        {

            if (cmbUnits.SelectedItem != null) {

                //MeasureUnitModel selectedMeasureUnitModel = cmbUnits.SelectedItem as MeasureUnitModel;
                //MeasureUnitModel selectedMeasureUnit = cmbUnits.SelectedIndex;
                MeasureUnitModel selectedMeasureUnit = cmbUnits.SelectedItem as MeasureUnitModel;
                lines.Add(new IngredientLineModel()
                {
                    Amount = Decimal.Parse(txtAmount.Text),
                    MeasureUnit = selectedMeasureUnit.Name,
                    MeasureUnitInt = selectedMeasureUnit.Number,
                    Ingredient = new IngredientModel(){ Name = txtIngredientname.Text }
                });
                
                //Setting the new list 
                grdIngredientLines.ItemsSource = lines;
                
                //Emptying fields after insertion
                txtIngredientname.Text = string.Empty;
                txtAmount.Text = string.Empty;
                cmbUnits.SelectedIndex = 0;
            }
        }

        private void btnAddIngr_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientLine();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //Remove Selected Ingredient from the list
            foreach (IngredientLineModel line in lines)
            {
                //if(line.Id ==)
                //{

                //}
            }
        }
    }    
}
