using Ertglr.Libraries.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoAppWebApi.ENTITIES.EntityClass;
using TodoAppWebApi.ENTITIES.ViewModels;
using TodoAppWebApi.MVC.Helpers;

namespace TodoAppWebApi.MVC.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            if (Session["login"] != null)
            {
                var user = Session["login"] as LoginResponseViewModel;
                var response = RestSharpHelperFactory.TodoApiHelper.Get<ResponseObject<List<TaskResponseViewModel>>>("/Task/GetTasks/" + user.Username);


                return View(response.Data.Result);

            }
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                LoginRequestViewModel postData = new LoginRequestViewModel()
                {
                    Email = model.Email,
                    Password = model.Password
                };

                var response = RestSharpHelperFactory.TodoApiHelper.Post<ResponseObject<LoginResponseViewModel>>("/User/Authenticate", postData);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (response.Data != null && response.Data.IsSuccess == true)
                    {
                        Session["login"] = response.Data.Result;
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterRequestViewModel user)
        {

            var response = RestSharpHelperFactory.TodoApiHelper.Post<ResponseObject<RegisterResponseViewModel>>("/User/PostUser", user);
            if (response.IsSuccessful)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(response);

        }

        public ActionResult UpdateUser(Guid id)
        {
            var response = RestSharpHelperFactory.TodoApiHelper.Get<List<ResponseObject<RegisterRequestViewModel>>>("/User/GetUserById/" + id);

            return View(response);
        }

        [HttpPost]
        public ActionResult UpdateUser(RegisterRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = RestSharpHelperFactory.TodoApiHelper.Put<ResponseObject<RegisterRequestViewModel>>("/User/PutUser", model);
                if (response.IsSuccessful)
                {
                    return RedirectToAction("Index");
                }

            }

            return View();
        }

    }
}