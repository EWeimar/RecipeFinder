﻿using RecipeFinder.Desktop.ApiHelpers;
using RecipeFinder.Desktop.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows;
using System.Windows.Input;

namespace RecipeFinder.Desktop
{
    /// <summary>
    /// Interaction logic for CreateRecipe.xaml
    /// </summary>
    public partial class UpdateRecipe : Window
    {
        RecipeModel recipe = null;

        ObservableCollection<IngredientLineModel> lines;
        ObservableCollection<ImageModel> images;

        RecipeCaller rc;

        string RowVersion = string.Empty;

        public UpdateRecipe(int RecipeId)
        {
            rc = new RecipeCaller(ConfigurationManager.AppSettings["RecipeFinderApiBaseUrl"]);
            InitializeComponent();
            recipe = GetRecipeById(RecipeId);
            SetData();
        }

        private void SetData()
        {

            // setting the current row version
            this.RowVersion = recipe.rowVer;

            // basic recipe information
            txtTitle.Text = recipe.Title;
            txtInstructions.Text = recipe.Instruction;

            // adds measure units to combobox
            AddMeasureUnits();

            // a list to edit the ingredient lines
            lines = new ObservableCollection<IngredientLineModel>(recipe.IngredientLines);

            // add already existing ingredient lines to the editable list
            grdIngredientLines.ItemsSource = lines;

            // a list to edit the images
            images = new ObservableCollection<ImageModel>(recipe.Images);

            // add already existing images to the editable image list
            grdImages.ItemsSource = images;
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

                lines.Add(new IngredientLineModel()
                {
                    Amount = Decimal.Parse(txtAmount.Text),
                    MeasureUnit = cmbUnits.Text,
                    MeasureUnitInt = cmbUnits.SelectedIndex,
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
            if(lines.Count == 0)
            {
                MessageBox.Show("Remember to add Ingredients");
                return;
            }
            if (string.IsNullOrEmpty(txtTitle.Text) || string.IsNullOrEmpty(txtInstructions.Text))
            {
                MessageBox.Show("Title and Instruction might not be empty"); 
            }           

            var recipeDTO = new
            {
                Id = recipe.Id,
                Title = txtTitle.Text,
                Instruction = txtInstructions.Text,
                IngredientLines = new List<object>(),
                Images = new List<object>(),
                User = new {Id = recipe.User.Id},
                RowVer = this.RowVersion
            };

            foreach (IngredientLineModel line in lines)
            {
                if (!string.IsNullOrEmpty(line.Ingredient.Name) && line.Amount > 0)
                {
                    var IngredientLineDTO = new {
                        Ingredient = new { Name = line.Ingredient.Name},
                        Amount = line.Amount,
                        MeasureUnit = line.MeasureUnitInt
                    };
                    recipeDTO.IngredientLines.Add(IngredientLineDTO);

                } 
            }

            foreach (ImageModel image in images)
            {
                if (!string.IsNullOrEmpty(image.FileName))
                {
                    recipeDTO.Images.Add(new { FileName = image.FileName });
                }
            }

            RFApiResult result = rc.UpdateRecipe(recipeDTO);

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {

                RecipeModel updatedRecipeModel = rc.FindByCondition("id", recipeDTO.Id);

                this.RowVersion = updatedRecipeModel.rowVer;
            }

            MessageBox.Show(result.Message);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            IngredientLineModel selectedIngredientLine = grdIngredientLines.SelectedItem as IngredientLineModel;

            if(lines.Count > 0) {
                if (lines.Contains(selectedIngredientLine)) {
                    lines.RemoveAt(grdIngredientLines.SelectedIndex);
                    grdIngredientLines.ItemsSource = lines;
                }                
            }
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {

            images.Add(new ImageModel()
            {
                FileName = txtImage.Text
            }) ;

                //Setting the new list 
                grdImages.ItemsSource = images;

                //Emptying fields after insertion
                txtImage.Text = string.Empty;          
        }

        private void btnRemoveImage_Click(object sender, RoutedEventArgs e)
        {
            ImageModel selectedImageModel = grdImages.SelectedItem as ImageModel;

            if (images.Count > 0)
            {
                if (images.Contains(selectedImageModel))
                {
                    images.RemoveAt(grdImages.SelectedIndex);
                    grdImages.ItemsSource = images;
                }
            }
        }
    }    
}
