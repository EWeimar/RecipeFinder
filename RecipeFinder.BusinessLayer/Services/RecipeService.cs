using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Lib;
using RecipeFinder.DataLayer;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var newRecipe = dbAccess.Recipes.AddAsync(new Recipe() {
                Id = 0,
                Title = recipe.Title,
                Slug = String.Format("{0}-{1}", recipe.User.Id, SlugHelper.GenerateSlug(recipe.Title)),
                Instruction = recipe.Instruction,
                UserId = recipe.User.Id,
                CreatedAt = DateTime.Now
            });

            //Create ingredientlines
            foreach (var ing in recipe.IngredientLines)
            {
                //Get or create the required ingredient
                Ingredient ingredient = dbAccess.Ingredients.FindByCondition(nameof(ing.Ingredient.Name), ing.Ingredient.Name).Result.FirstOrDefault();
                if (ingredient == null)
                {
                    var ingredientToBeCreated = new Ingredient();
                    ingredientToBeCreated.Id = 0;
                    ingredientToBeCreated.Name = ing.Ingredient.Name;

                    ingredient = dbAccess.Ingredients.AddAsync(ingredientToBeCreated).Result;
                }
                //Create the ingredientline
                IngredientLine ingredientLine = new IngredientLine();
                ingredientLine.Id = 0;
                ingredientLine.RecipeId = newRecipe.Id;
                ingredientLine.IngredientId = ingredient.Id;
                ingredientLine.Amount = ing.Amount;
                ingredientLine.MeasureUnit = ing.MeasureUnit;

                var newIngredientLine = dbAccess.IngredientLines.AddAsync(ingredientLine);
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

                    var newImage = dbAccess.Images.AddAsync(imageToBeCreated);
                }
            }
        }

        public RecipeDTO Get(int id)
        {
            var getRecipe = dbAccess.Recipes.GetByIdAsync(id).Result;
            //Check if recipe exists
            if(getRecipe == null)
            {
                throw new ArgumentNullException("The recipe could not be found!");
            }
            //Convert user info to DTO
            var user = dbAccess.Users.GetByIdAsync(getRecipe.UserId).Result;
            UserDTO uResult = new UserDTO();
            uResult.Id = user.Id;
            uResult.Username = user.Username;

            //Create list for ingredientlines
            List<IngredientLineDTO> ilResults = new List<IngredientLineDTO>();
            //Fetch ingredientlines in DB based on recipeId
            var ingredientLines = dbAccess.IngredientLines.FindByCondition(nameof(IngredientLine.RecipeId), getRecipe.Id).Result.ToList();
            //Loop over ingredientlines in DB, convert to DTO and add to new list of ingredientlines
            foreach (var il in ingredientLines)
            {
                IngredientLineDTO ingredientLineDTO = new IngredientLineDTO();
                var ingredient = dbAccess.Ingredients.GetByIdAsync(il.IngredientId).Result;

                IngredientDTO ingredientDTO = new IngredientDTO();
                ingredientDTO.Id = ingredient.Id;
                ingredientDTO.Name = ingredient.Name;
                ingredientLineDTO.Ingredient = ingredientDTO;
                ingredientLineDTO.Amount = il.Amount;
                ingredientLineDTO.MeasureUnit = il.MeasureUnit;

                ilResults.Add(ingredientLineDTO);
            }

            //Create list for images
            List<ImageDTO> imResults = new List<ImageDTO>();
            //Fetch images in DB based on recipeId
            var images = dbAccess.Images.FindByCondition(nameof(Image.RecipeId), getRecipe.Id).Result.ToList();
            //Loop over images in DB, convert to DTO and add to new list of images
            foreach (var im in images)
            {
                ImageDTO imageDTO = new ImageDTO();
                imageDTO.Id = im.Id;
                imageDTO.FileName = im.FileName;

                imResults.Add(imageDTO);
            }

            //Convert recipe to DTO and return result
            RecipeDTO result = new RecipeDTO();
            result.User = uResult;
            result.Id = getRecipe.Id;
            result.Title = getRecipe.Title;
            result.Instruction = getRecipe.Instruction;
            result.CreatedAt = getRecipe.CreatedAt;
            result.IngredientLines = ilResults;
            result.Images = imResults;

            return result;
        }

        public List<RecipeDTO> GetAll()
        {

            List<RecipeDTO> recipeList = new List<RecipeDTO>();
            var resultSet = dbAccess.Recipes.GetAllAsync().Result.ToList();

            if(resultSet == null)
            {
                throw new ArgumentNullException("No recipes were found!");
            }

            foreach (var item in resultSet)
            {
                //Convert user info to DTO
                var user = dbAccess.Users.GetByIdAsync(item.UserId).Result;
                UserDTO uResult = new UserDTO();
                uResult.Id = user.Id;
                uResult.Username = user.Username;

                //Create list for ingredientlines
                List<IngredientLineDTO> ilResults = new List<IngredientLineDTO>();
                //Fetch ingredientlines in DB based on recipeId
                var ingredientLines = dbAccess.IngredientLines.FindByCondition(nameof(IngredientLine.RecipeId), item.Id).Result.ToList();
                //Loop over ingredientlines in DB, convert to DTO and add to new list of ingredientlines
                foreach (var il in ingredientLines)
                {
                    IngredientLineDTO ingredientLineDTO = new IngredientLineDTO();
                    var ingredient = dbAccess.Ingredients.GetByIdAsync(il.IngredientId).Result;

                    IngredientDTO ingredientDTO = new IngredientDTO();
                    ingredientDTO.Name = ingredient.Name;
                    ingredientDTO.Id = ingredient.Id;
                    ingredientLineDTO.Id = il.Id;
                    ingredientLineDTO.Ingredient = ingredientDTO;
                    ingredientLineDTO.Amount = il.Amount;
                    ingredientLineDTO.MeasureUnit = il.MeasureUnit;
                    
                    ilResults.Add(ingredientLineDTO);
                }

                //Create list for images
                List<ImageDTO> imResults = new List<ImageDTO>();
                //Fetch images in DB based on recipeId
                var images = dbAccess.Images.FindByCondition(nameof(Image.RecipeId), item.Id).Result.ToList();
                //Loop over images in DB, convert to DTO and add to new list of images
                foreach (var im in images)
                {
                    ImageDTO imageDTO = new ImageDTO();
                    imageDTO.Id = im.Id;
                    imageDTO.FileName = im.FileName;

                    imResults.Add(imageDTO);
                }

                //Convert recipe to DTO and return result
                RecipeDTO result = new RecipeDTO();
                result.User = uResult;
                result.Id = item.Id;
                result.Title = item.Title;
                result.Instruction = item.Instruction;
                result.CreatedAt = item.CreatedAt;
                result.IngredientLines = ilResults;
                result.Images = imResults;

                recipeList.Add(result);
            }
            return recipeList;

        }

        public void Update(RecipeDTO input)
        {
            Validation(input);

            var updateRecipe = dbAccess.Recipes.GetByIdAsync(input.Id).Result;
            
            //Check if recipe exists
            if(updateRecipe == null)
            {
                throw new ArgumentNullException("The recipe could not be found!");
            }

            //Update the recipe properties
            updateRecipe.Title = input.Title;
            updateRecipe.Slug = input.Slug;
            updateRecipe.Instruction = input.Instruction;


            //Fetch ingredientLines in DB based on recipeId
            var updateIngredientLines = dbAccess.IngredientLines.FindByCondition(nameof(IngredientLine.RecipeId), updateRecipe.Id).Result.ToList();

            //Update ingredientLines
            UpdateIngredientLine(input, updateIngredientLines);

            //Fetch images in DB based on recipeId
            var updateImages = dbAccess.Images.FindByCondition(nameof(Image.RecipeId), updateRecipe.Id).Result.ToList();
            //Update images
            UpdateImage(input, updateImages);

            //Update the recipe
            dbAccess.Recipes.UpdateAsync(updateRecipe);
        }

        public void Delete(RecipeDTO recipe)
        {
            //Delete from the bottom and up. 
            //Images > IngredientLine > Recipe due to foreign key constraints in DB

            var deleteRecipe = dbAccess.Recipes.GetByIdAsync(recipe.Id).Result;
            //Check if recipe exists
            if(deleteRecipe == null)
            {
                throw new ArgumentNullException("The recipe could not be found!");
            }

            //Fetch images in DB based on recipeId
            var deleteImages = dbAccess.Images.FindByCondition(nameof(Image.RecipeId), deleteRecipe.Id).Result.ToList();
            //Delete images
            foreach (var item in deleteImages)
            {
                dbAccess.Images.DeleteAsync(item.Id);
            }

            //Fetch ingredientLines in DB based on recipeId
            var deleteIngredientLines = dbAccess.IngredientLines.FindByCondition(nameof(IngredientLine.RecipeId), deleteRecipe.Id).Result.ToList();
            //Delete ingredientLines
            foreach (var item in deleteIngredientLines)
            {
                dbAccess.IngredientLines.DeleteAsync(item.Id);
            }

            //Delete recipe
            dbAccess.Recipes.DeleteAsync(deleteRecipe.Id);
                
            //Check if recipe has been deleted
            if (dbAccess.Recipes.GetByIdAsync(deleteRecipe.Id) == null)
            {
                Console.WriteLine("Recipe has been deleted!");
            }
        }


        //Validation of inbound recipe(in DTO format)
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
            if(dbAccess.Users.GetByIdAsync(recipe.User.Id) == null)
            {
                throw new ArgumentException("UserId does not exist!");
            }
        }

        private void UpdateIngredientLine(RecipeDTO dto, List<IngredientLine> existing)
        {
            //Add ingredient - Identify ingredientLine to be created
            List<IngredientLineDTO> toBeCreated = new List<IngredientLineDTO>();
            foreach (var item in dto.IngredientLines)
            {
                if (existing.Any(ex => ex.Id == item.Id))
                {
                    //Nothing happens!
                }
                else
                {
                    toBeCreated.Add(item);
                }
            }

            //Update ingredient - Identify ingredientLine to be updated
            List<IngredientLineDTO> toBeUpdated = new List<IngredientLineDTO>(); 
            foreach (var item in dto.IngredientLines)
            {
                if(existing.Any(ex => ex.Id == item.Id))
                {
                    toBeUpdated.Add(item);
                }
                //else - Nothing happens!
            }

            //Delete ingredient - Identify ingredientLine to be deleted
            //Type is not DTO, as we delete from the database
            List<IngredientLine> toBeDeleted = new List<IngredientLine>();
            foreach (var e in existing)
            {
                if(toBeCreated.Any(ex => ex.Id == e.Id) || toBeUpdated.Any(ex => ex.Id == e.Id))
                {
                    //Nothing happens!
                }
                else
                {
                    toBeDeleted.Add(e);
                }
            }

            //Create new ingredientLine
            foreach (var item in toBeCreated)
            {
                //Get or create the required ingredient
                Ingredient ingredient = dbAccess.Ingredients.FindByCondition(nameof(Ingredient.Name), item.Ingredient.Name).Result.ToList().FirstOrDefault();
                if (ingredient == null)
                {
                    var ingredientToBeCreated = new Ingredient();
                    ingredientToBeCreated.Id = 0;
                    ingredientToBeCreated.Name = item.Ingredient.Name;

                    ingredient = dbAccess.Ingredients.AddAsync(ingredientToBeCreated).Result;
                }

                //Create new ingredientLine
                IngredientLine il = new IngredientLine();
                il.Id = 0;
                il.RecipeId = dto.Id;
                il.IngredientId = ingredient.Id;
                il.Amount = item.Amount;
                il.MeasureUnit = item.MeasureUnit;

                //Send to DB
                dbAccess.IngredientLines.AddAsync(il);
            }

            //Update existing ingredientLine
            foreach (var item in toBeUpdated)
            {
                IngredientLine il = dbAccess.IngredientLines.GetByIdAsync(item.Id).Result;

                il.Amount = item.Amount;
                il.MeasureUnit = item.MeasureUnit;

                //Send to DB
                dbAccess.IngredientLines.UpdateAsync(il);
            }

            //Delete existing ingredientLine
            foreach (var item in toBeDeleted)
            {
                dbAccess.IngredientLines.DeleteAsync(item.Id);
            }
        }

        private void UpdateImage(RecipeDTO dto, List<Image> existing)
        {
            //Add image - Identify images to be created
            List<ImageDTO> toBeCreated = new List<ImageDTO>();
            foreach (var item in dto.Images)
            {
                if(existing.Any(ex => ex.Id == item.Id))
                {
                    //Nothing happens!
                }
                else
                {
                    toBeCreated.Add(item);
                }
            }

            //Remaining image - Identify images to remain as is
            List<ImageDTO> toRemain = new List<ImageDTO>();
            foreach (var item in dto.Images)
            {
                if(existing.Any(ex => ex.Id == item.Id))
                {
                    toRemain.Add(item);
                }
                //else - Nothing happens!
            }

            //Delete image - Identify images to be deleted
            List<Image> toBeDeleted = new List<Image>();
            foreach (var e in existing)
            {
                if(toBeCreated.Any(ex => ex.Id == e.Id) || toRemain.Any(ex => ex.Id == e.Id))
                {
                    //Nothing happens
                }
                else
                {
                    toBeDeleted.Add(e);
                }
            }

            //Create new image
            foreach (var item in toBeCreated)
            {
                //Create new image
                Image image = new Image();
                image.Id = 0;
                image.RecipeId = dto.Id;
                image.FileName = item.FileName;

                //Send to DB
                dbAccess.Images.AddAsync(image);
            }

            //Delete image
            foreach (var item in toBeDeleted)
            {
                dbAccess.Images.DeleteAsync(item.Id);
            }
        }
    }
}
