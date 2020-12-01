using RecipeFinder.WebClient.ApiHelpers;
using RecipeFinder.WebClient.Models;
using RecipeFinder.WebClient.ViewModels;
using System;
using System.Collections.Generic;
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
            UserCaller uc = new UserCaller("https://localhost:44320/api");
            RFApiResult response = uc.CreateUser(new User() { Username = vm.Username, Email = vm.Email, Password = vm.Password});

            if (response.Success)
            {
                SetFlash(Models.FlashMessageType.Success, "Tillykke din bruger blev oprettet.");
                //SetFlash(Models.FlashMessageType.Success, response.Message);
            } else
            {
                SetFlash(Models.FlashMessageType.Danger, "Der skete en fejl.");

                //SetFlash(Models.FlashMessageType.Danger, response.Message);
            }

            //return View("Create");

            return Redirect(Url.Action("Create", "User"));
        }
    }
}