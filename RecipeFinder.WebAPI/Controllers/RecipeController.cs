using RecipeFinder.BusinessLayer.Interfaces;
using RecipeFinder.BusinessLayer.Services;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecipeFinder.WebAPI.Controllers
{
    public class RecipeController : ApiBaseController
    {
        private IRecipeService RecipeService;

        public IngredientLineDTO IngredientLines { get; private set; }

        public RecipeController()
        {
            RecipeService = new RecipeService();
        }
        
        public void CreateRecipe(RecipeDTO recipe)
        {
            RecipeService.Create(recipe);
        }
        public RecipeDTO GetRecipe(int id)
        {
            //RecipeDTO obj = new RecipeDTO();
            //obj.Id = 0;
            //obj.User = new UserDTO() { Id = 1 };
            //obj.Title = "Sandwich";
            //obj.Slug = "Sandwich-Slug";
            //obj.Instruction = "Lav en sandwich";
            //obj.IngredientLines = new List<IngredientLineDTO>()
            //{
            //    new IngredientLineDTO()
            //    {
            //        Id = 0,
            //        Ingredient = new IngredientDTO()
            //        {
            //            Id = 0,
            //            Name = "Brød"
            //        },
            //        Amount = 1,
            //        MeasureUnit = MeasureUnit.Stk
            //    }
            //};
            //obj.Images = new List<ImageDTO>()
            //{
            //    new ImageDTO()
            //    {
            //        Id = 0,
            //        FileName = "FlotSandwich.jpg"
            //    },
            //    new ImageDTO()
            //    {
            //        Id = 0,
            //        FileName = "LækkerSandwich.png"
            //    }
            //};
            //RecipeService.Create(obj);
            return RecipeService.Get(id);
        }
        public List<RecipeDTO> GetAll()
        {
            return RecipeService.GetAll();
        }
        public void UpdateRecipe(RecipeDTO recipe)
        {
            RecipeService.Update(recipe);
        }
        public void DeleteRecipe(RecipeDTO recipe)
        {
            RecipeService.Delete(recipe);
        }
    }
}
