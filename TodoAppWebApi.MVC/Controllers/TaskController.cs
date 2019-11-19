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
    public class TaskController : Controller
    {
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TaskRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = RestSharpHelperFactory.TodoApiHelper.Post<TaskResponseViewModel>("/Task/CreateTask",model);
                    return RedirectToAction("Index","User");

            }

            return View(model);
        }


        public ActionResult Update(int id)
        {
           var task = RestSharpHelperFactory.TodoApiHelper.Get<ResponseObject<TaskResponseViewModel>>("/Task/GetTaskById/"+id);
            
            return View(task.Data.Result);
        }

        [HttpPost]
        public ActionResult Update(TaskRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = RestSharpHelperFactory.TodoApiHelper.Put<TaskResponseViewModel>("/Task/UpdateTask", model);

                if (response.IsSuccessful==true && response.StatusCode==System.Net.HttpStatusCode.OK)
                {
                     return RedirectToAction("Index", "User");

                }

                return View(response.ErrorMessage);

            }

            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var task = RestSharpHelperFactory.TodoApiHelper.Get<TaskResponseViewModel>("/Task/GetTaskById/" + id);

            return View();
        }

        [HttpPost]
        public ActionResult Delete(TaskRequestViewModel model)
        {
            var response = RestSharpHelperFactory.TodoApiHelper.Delete<TaskResponseViewModel>("/Task/DeleteTask/"+model.ID);

            return View();
        }
    }
}