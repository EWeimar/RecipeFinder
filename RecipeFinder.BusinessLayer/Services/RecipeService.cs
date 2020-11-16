using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.DataLayer;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.BusinessLayer.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork dbAccess;    
        public RecipeService()
        {
            dbAccess = new UnitOfWork();
        }

        public void Create(RecipeDTO recipe)
        {
            //Validate recipe
            Validation(recipe);

            //Create recipe in DB
            Recipe r = new Recipe();
            r.Id = 0;
            r.Title = recipe.Title;
            r.Slug = recipe.Slug;
            r.Instruction = recipe.Instruction;
            r.UserId = recipe.User.Id;
            r.CreatedAt = DateTime.Now;
            
            var newRecipe = dbAccess.Recipes.Create(r);

            //Create ingredientlines
            foreach (var ing in recipe.IngredientLines)
            {
                //Get or create the required ingredient
                Ingredient ingredient = dbAccess.Ingredients.GetAll(nameof(ing.Ingredient.Name), ing.Ingredient.Name).FirstOrDefault();
                if (ingredient == null)
                {
                    var ingredientToBeCreated = new Ingredient();
                    ingredientToBeCreated.Id = 0;
                    ingredientToBeCreated.Name = ing.Ingredient.Name;

                    ingredient = dbAccess.Ingredients.Create(ingredientToBeCreated);
                }
                //Create the ingredientline
                IngredientLine ingredientLine = new IngredientLine();
                ingredientLine.Id = 0;
                ingredientLine.RecipeId = newRecipe.Id;
                ingredientLine.IngredientId = ingredient.Id;
                ingredientLine.Amount = ing.Amount;
                ingredientLine.MeasureUnit = ing.MeasureUnit;

                var newIngredientLine = dbAccess.IngredientLines.Create(ingredientLine);
            }
            //Create images (if any)
            if (recipe.Images != null)
            {
                foreach (var image in recipe.Images)
                {
                    var imageToBeCreated = new Image();
                    imageToBeCreated.Id = 0;
                    imageToBeCreated.RecipeId = newRecipe.Id;
                    imageToBeCreated.FileName = image.FileName;

                    var newImage = dbAccess.Images.Create(imageToBeCreated);
                }
            }
        }

        public RecipeDTO Get(int id)
        {
            var getRecipe = dbAccess.Recipes.Get(id);
            //Check if recipe exists
            if(getRecipe == null)
            {
                throw new ArgumentNullException("The recipe could not be found!");
            }
            //Convert user info to DTO
            var user = dbAccess.Users.Get(getRecipe.UserId);
            UserDTO uResult = new UserDTO();
            uResult.Username = user.Username;

            //Create list for ingredientlines
            List<IngredientLineDTO> ilResults = new List<IngredientLineDTO>();
            //Fetch ingredientlines in DB based on recipeId
            var ingredientLines = dbAccess.IngredientLines.GetAll(nameof(IngredientLine.RecipeId), getRecipe.Id);
            //Loop over ingredientlines in DB, convert to DTO and add to new list of ingredientlines
            foreach (var il in ingredientLines)
            {
                IngredientLineDTO ingredientLineDTO = new IngredientLineDTO();
                var ingredients = dbAccess.Ingredients.Get(il.IngredientId);

                IngredientDTO ingredientDTO = new IngredientDTO();
                ingredientDTO.Name = ingredients.Name;
                ingredientLineDTO.Ingredient = ingredientDTO;
                ingredientLineDTO.Amount = il.Amount;
                ingredientLineDTO.MeasureUnit = il.MeasureUnit;

                ilResults.Add(ingredientLineDTO);
            }

            //Create list for images
            List<ImageDTO> imResults = new List<ImageDTO>();
            //Fetch images in DB based on recipeId
            var images = dbAccess.Images.GetAll(nameof(Image.RecipeId), getRecipe.Id);
            //Loop over images in DB, convert to DTO and add to new list of images
            foreach (var im in images)
            {
                ImageDTO imageDTO = new ImageDTO();
                imageDTO.FileName = im.FileName;

                imResults.Add(imageDTO);
            }

            //Convert recipe to DTO and return result
            RecipeDTO result = new RecipeDTO();
            result.User = uResult;
            result.Title = getRecipe.Title;
            result.Instruction = getRecipe.Instruction;
            result.CreatedAt = getRecipe.CreatedAt;
            result.IngredientLines = ilResults;
            result.Images = imResults;

            return result;
        }

        //Validation of inbound recipe(in DTO format2)
        private void Validation(RecipeDTO recipe)
        {
            if(recipe == null)
            {
                throw new ArgumentNullException("Recipe cannot be null!");
            } 
            else if(recipe.User == null)
            {
                throw new ArgumentNullException("User cannot be null!");
            }
            else if(recipe.Title == null)
            {
                throw new ArgumentNullException("Title cannot be null!");
            }
            else if(recipe.Slug == null)
            {
                throw new ArgumentNullException("Slug cannot be null!");
            }
            else if(recipe.Instruction == null)
            {
                throw new ArgumentNullException("Instruction cannot be null!");
            }
            else if(recipe.User.Id <= 0)
            {
                throw new ArgumentException("UserId cannot be zero or less!");
            }
            
            if(recipe.Images != null && recipe.Images.Count > 0)
            {
                foreach (var i in recipe.Images)
                {
                    if(i.FileName == null)
                    {
                        throw new ArgumentNullException("FileName cannot be null!");
                    }
                }
            }

            if(recipe.IngredientLines == null)
            {
                throw new ArgumentNullException("IngredientLines cannot be null!");
            }
            else if(recipe.IngredientLines.Count == 0)
            {
                throw new ArgumentException("IngredientLines cannont be zero!");
            }
            else
            {
                foreach (var il in recipe.IngredientLines)
                {
                    if (il.Amount <= 0.0m)
                    {
                        throw new ArgumentException("Amount cannot be zero or less!");
                    }
                    else if (il.Ingredient == null)
                    {
                        throw new ArgumentNullException("Ingredient cannot be null!");
                    }
                    else if (il.Ingredient.Name == null)
                    {
                        throw new ArgumentNullException("Ingredient.Name cannot be null!");
                    }
                }
            }

            //Ensure users exists
            if(dbAccess.Users.Get(recipe.User.Id) == null)
            {
                throw new ArgumentException("UserId does not exist!");
            }
        }
    }
}
