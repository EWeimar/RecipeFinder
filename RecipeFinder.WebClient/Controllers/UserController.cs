using RecipeFinder.WebClient.ApiHelpers;
using RecipeFinder.WebClient.Models;
using RecipeFinder.WebClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace RecipeFinder.WebClient.Controllers
{
    public class UserController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitCreate(CreateUserViewModel vm)
        {
            UserCaller uc = new UserCaller(ConfigurationManager.AppSettings["RecipeFinderApiBaseUrl"]);
            RFApiResult response = uc.CreateUser(new User() { Username = vm.Username, Email = vm.Email, Password = vm.Password});

            if (response.Success)
            {
                SetFlash(Models.FlashMessageType.Success, response.Message);
            } else
            {
                SetFlash(Models.FlashMessageType.Danger, response.Message);
            }

            return Redirect(Url.Action("Create", "User"));
        }
    }
}