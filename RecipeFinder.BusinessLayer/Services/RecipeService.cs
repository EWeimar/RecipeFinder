﻿using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Lib;
using RecipeFinder.DataLayer;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

        public async Task<RecipeDTO> AddAsync(RecipeDTO recipe)
        {
            //Validate recipe
            Validation(recipe);

            //Create recipe in DB
            var newRecipe = await dbAccess.Recipes.AddAsync(new Recipe()
            {
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
                Ingredient ingredient = (await dbAccess.Ingredients.FindByCondition(nameof(ing.Ingredient.Name), ing.Ingredient.Name)).FirstOrDefault();
                if (ingredient == null)
                {
                    var ingredientToBeCreated = new Ingredient();
                    ingredientToBeCreated.Id = 0;
                    ingredientToBeCreated.Name = ing.Ingredient.Name;

                    ingredient = await dbAccess.Ingredients.AddAsync(ingredientToBeCreated);
                }
                //Create the ingredientline
                IngredientLine ingredientLine = new IngredientLine();
                ingredientLine.Id = 0;
                ingredientLine.RecipeId = newRecipe.Id;
                ingredientLine.IngredientId = ingredient.Id;
                ingredientLine.Amount = ing.Amount;
                ingredientLine.MeasureUnit = ConvertToEnum(ing.MeasureUnitText);

                var newIngredientLine = await dbAccess.IngredientLines.AddAsync(ingredientLine);
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

                    var newImage = await dbAccess.Images.AddAsync(imageToBeCreated);
                }
            }

            return await GetByIdAsync(newRecipe.Id);
        }

        public async Task<RecipeDTO> GetByIdAsync(int id)
        {
            var getRecipe = await dbAccess.Recipes.GetByIdAsync(id);
            //Check if recipe exists
            if (getRecipe == null)
            {
                throw new ArgumentNullException("The recipe could not be found!");
            }

            //Convert user info to DTO
            var user = await dbAccess.Users.GetByIdAsync(getRecipe.UserId);
            UserDTO uResult = new UserDTO();
            uResult.Id = user.Id;
            uResult.Username = user.Username;

            //Create list for ingredientlines
            List<IngredientLineDTO> ilResults = new List<IngredientLineDTO>();
            //Fetch ingredientlines in DB based on recipeId
            var ingredientLines = (await dbAccess.IngredientLines.FindByCondition(nameof(IngredientLine.RecipeId), getRecipe.Id)).ToList();
            //Loop over ingredientlines in DB, convert to DTO and add to new list of ingredientlines
            foreach (var il in ingredientLines)
            {
                IngredientLineDTO ingredientLineDTO = new IngredientLineDTO();
                var ingredient = await dbAccess.Ingredients.GetByIdAsync(il.IngredientId);

                IngredientDTO ingredientDTO = new IngredientDTO();
                ingredientDTO.Id = ingredient.Id;
                ingredientDTO.Name = ingredient.Name;
                ingredientLineDTO.Ingredient = ingredientDTO;
                ingredientLineDTO.Amount = il.Amount;
                ingredientLineDTO.MeasureUnit = il.MeasureUnit;
                ingredientLineDTO.MeasureUnitInt = (int)il.MeasureUnit;

                ilResults.Add(ingredientLineDTO);
            }

            //Create list for images
            List<ImageDTO> imResults = new List<ImageDTO>();
            //Fetch images in DB based on recipeId
            var images = (await dbAccess.Images.FindByCondition(nameof(Image.RecipeId), getRecipe.Id)).ToList();
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
            result.Slug = getRecipe.Slug;
            result.Instruction = getRecipe.Instruction;
            result.CreatedAt = getRecipe.CreatedAt;
            result.IngredientLines = ilResults;
            result.RowVer = getRecipe.RowVer;
            result.Images = imResults;

            return result;
        }

        public async Task<IEnumerable<RecipeDTO>> GetAllAsync()
        {

            List<RecipeDTO> recipeList = new List<RecipeDTO>();
            var resultSet = (await dbAccess.Recipes.GetAllAsync()).ToList();

            if (resultSet == null)
            {
                throw new ArgumentNullException("No recipes were found!");
            }

            foreach (var item in resultSet)
            {
                //Convert user info to DTO
                var user = await dbAccess.Users.GetByIdAsync(item.UserId);
                UserDTO uResult = new UserDTO();
                uResult.Id = user.Id;
                uResult.Username = user.Username;

                //Create list for ingredientlines
                List<IngredientLineDTO> ilResults = new List<IngredientLineDTO>();
                //Fetch ingredientlines in DB based on recipeId
                var ingredientLines = (await dbAccess.IngredientLines.FindByCondition(nameof(IngredientLine.RecipeId), item.Id)).ToList();
                //Loop over ingredientlines in DB, convert to DTO and add to new list of ingredientlines
                foreach (var il in ingredientLines)
                {
                    IngredientLineDTO ingredientLineDTO = new IngredientLineDTO();
                    var ingredient = await dbAccess.Ingredients.GetByIdAsync(il.IngredientId);

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
                var images = (await dbAccess.Images.FindByCondition(nameof(Image.RecipeId), item.Id)).ToList();
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
                result.Slug = item.Slug;
                result.Instruction = item.Instruction;
                result.CreatedAt = item.CreatedAt;
                result.IngredientLines = ilResults;
                result.Images = imResults;
                result.RowVer = item.RowVer;

                recipeList.Add(result);
            }
            return recipeList;

        }

        public async Task<int> UpdateAsync(RecipeDTO recipeDTO)
        {
            Recipe recipe = await dbAccess.Recipes.GetByIdAsync(recipeDTO.Id);

            recipe.Title = recipeDTO.Title;
            recipe.Instruction = recipeDTO.Instruction;
            recipe.RowVer = recipeDTO.RowVer; // convert to byte array

            int updateResult = await dbAccess.Recipes.UpdateAsync(recipe);

            if (updateResult == 0)
            {
                return 0;
            }

            var existingIngredientLines = (await dbAccess.IngredientLines.FindByCondition(nameof(IngredientLine.RecipeId), recipeDTO.Id)).ToList();
            var existingImages = (await dbAccess.Images.FindByCondition(nameof(Image.RecipeId), recipeDTO.Id)).ToList();

            // delete old ingredient lines on the recipe
            foreach (IngredientLine existingIngredientLine in existingIngredientLines)
            {
                await dbAccess.IngredientLines.DeleteAsync(existingIngredientLine.Id);
            }

            // delete old images on the recipe
            foreach (Image existingImage in existingImages)
            {
                await dbAccess.Images.DeleteAsync(existingImage.Id);
            }

            // loop thru the ingredient lines sent by the user to be inserted
            foreach (IngredientLineDTO ingredientLineDTO in recipeDTO.IngredientLines)
            {

                // check ingredient name exists
                var existingIngredientByName = await dbAccess.Ingredients.FindByCondition("Name", ingredientLineDTO.Ingredient.Name);

                IngredientLine newIngredientLine = new IngredientLine();

                if (existingIngredientByName.Any())
                {
                    // if the ingredient already exists by name, assign the existing ingredient to the ingredient line
                    newIngredientLine.IngredientId = existingIngredientByName.Single().Id;
                }
                else
                {
                    // the ingredient does not exist by name, we create it, get the new IngredientId and assign it to the ingredient line
                    Ingredient newIngredient = new Ingredient();
                    newIngredient.Name = ingredientLineDTO.Ingredient.Name;

                    var res = await dbAccess.Ingredients.AddAsync(newIngredient);

                    newIngredientLine.IngredientId = res.Id;
                }

                newIngredientLine.RecipeId = recipe.Id;
                newIngredientLine.Amount = ingredientLineDTO.Amount;
                newIngredientLine.MeasureUnit = (MeasureUnit)ingredientLineDTO.MeasureUnit;

                // saves the data to database
                await dbAccess.IngredientLines.AddAsync(newIngredientLine);
            }

            foreach (ImageDTO imageDTO in recipeDTO.Images)
            {
                Image image = new Image();
                image.FileName = imageDTO.FileName;
                image.RecipeId = recipe.Id;

                await dbAccess.Images.AddAsync(image);
            }

            return updateResult;
        }

        public async Task<int> DeleteAsync(RecipeDTO recipe)
        {
            //Delete from the bottom and up. 
            //Images > IngredientLine > Recipe due to foreign key constraints in DB

            var deleteRecipe = await dbAccess.Recipes.GetByIdAsync(recipe.Id);
            //Check if recipe exists
            if (deleteRecipe == null)
            {
                throw new ArgumentNullException("The recipe could not be found!");
            }

            //Fetch images in DB based on recipeId
            var deleteImages = (await dbAccess.Images.FindByCondition(nameof(Image.RecipeId), deleteRecipe.Id)).ToList();
            //Delete images
            foreach (var item in deleteImages)
            {
                await dbAccess.Images.DeleteAsync(item.Id);
            }

            //Fetch ingredientLines in DB based on recipeId
            var deleteIngredientLines = (await dbAccess.IngredientLines.FindByCondition(nameof(IngredientLine.RecipeId), deleteRecipe.Id)).ToList();
            //Delete ingredientLines
            foreach (var item in deleteIngredientLines)
            {
                await dbAccess.IngredientLines.DeleteAsync(item.Id);
            }

            //Delete recipe
            return await dbAccess.Recipes.DeleteAsync(deleteRecipe.Id);

        }

        public async Task<List<object>> GetAllMeasureUnits()
        {
            List<object> res = new List<object>();

            foreach (int i in Enum.GetValues(typeof(MeasureUnit)))
            { 
                String name = Enum.GetName(typeof(MeasureUnit), i);
                res.Add(new
                {
                    Name = name,
                    Number = i
                });
            }

            return res;
        }

        public async Task<RecipeDTO> FindByCondition(string propName, object value)
        {
            var result = await dbAccess.Recipes.FindByCondition(propName, value);
            Recipe r = result.Single();

            return await GetByIdAsync(r.Id);
        }

        //Validation of inbound recipe(in DTO format)
        private void Validation(RecipeDTO recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException("Recipe cannot be null!");
            }
            else if (recipe.User == null)
            {
                throw new ArgumentNullException("User cannot be null!");
            }
            else if (recipe.Title == null)
            {
                throw new ArgumentNullException("Title cannot be null!");
            }
            else if (recipe.Instruction == null)
            {
                throw new ArgumentNullException("Instruction cannot be null!");
            }
            else if (recipe.User.Id <= 0)
            {
                throw new ArgumentException("UserId cannot be zero or less!");
            }

            if (recipe.Images != null && recipe.Images.Count > 0)
            {
                foreach (var i in recipe.Images)
                {
                    if (i.FileName == null)
                    {
                        throw new ArgumentNullException("FileName cannot be null!");
                    }
                }
            }

            if (recipe.IngredientLines == null)
            {
                throw new ArgumentNullException("IngredientLines cannot be null!");
            }
            else if (recipe.IngredientLines.Count == 0)
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
            if (dbAccess.Users.GetByIdAsync(recipe.User.Id) == null)
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
                if (existing.Any(ex => ex.Id == item.Id))
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
                if (toBeCreated.Any(ex => ex.Id == e.Id) || toBeUpdated.Any(ex => ex.Id == e.Id))
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
                il.MeasureUnit = ConvertToEnum(item.MeasureUnitText);

                //Send to DB
                dbAccess.IngredientLines.AddAsync(il);
            }

            //Update existing ingredientLine
            foreach (var item in toBeUpdated)
            {
                IngredientLine il = dbAccess.IngredientLines.GetByIdAsync(item.Id).Result;

                il.Amount = item.Amount;
                il.MeasureUnit = ConvertToEnum(item.MeasureUnitText);

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
                if (existing.Any(ex => ex.Id == item.Id))
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
                if (existing.Any(ex => ex.Id == item.Id))
                {
                    toRemain.Add(item);
                }
                //else - Nothing happens!
            }

            //Delete image - Identify images to be deleted
            List<Image> toBeDeleted = new List<Image>();
            foreach (var e in existing)
            {
                if (toBeCreated.Any(ex => ex.Id == e.Id) || toRemain.Any(ex => ex.Id == e.Id))
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

        private MeasureUnit ConvertToEnum(string input)
        {
            if (int.TryParse(input, out int result))
            {
                foreach (var item in Enum.GetValues(typeof(MeasureUnit)))
                {
                    if (result == (int)((MeasureUnit)item))
                    {
                        return (MeasureUnit)item;
                    }
                }
            }
            else
            {
                foreach (var item in Enum.GetValues(typeof(MeasureUnit)))
                {
                    //Text will contain "-" for MeasureUnit.None
                    //If enum member does not exist, it will return null
                    string text = GetEnumMemberAttrValue<MeasureUnit>((MeasureUnit)item);

                    if (input == text)
                    {
                        return (MeasureUnit)item;
                    }
                }
            }
            //If no enum member is found, returns 'None' as default.
            return MeasureUnit.None;
        }


        //Taken from: https://stackoverflow.com/questions/27372816/how-to-read-the-value-for-an-enummember-attribute
        //Gets enumMember annotation value
        //If we want the enum value 'none' it will return "-" 
        public string GetEnumMemberAttrValue<T>(T enumVal)
        {
            var enumType = typeof(T);
            var memInfo = enumType.GetMember(enumVal.ToString());
            var attr = memInfo.FirstOrDefault()?.GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
            if (attr != null)
            {
                return attr.Value;
            }

            return null;
        }

    }
}
