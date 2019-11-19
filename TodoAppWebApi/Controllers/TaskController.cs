using Ertglr.Libraries.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoAppWebApi.DAL.Context;
using TodoAppWebApi.ENTITIES.EntityClass;
using TodoAppWebApi.ENTITIES.ViewModels;

namespace TodoAppWebApi.Controllers
{
    public class TaskController : ApiController
    {
        TodoContext db = null;
        public TaskController()
        {
            db = new TodoContext();
        }


        public ResponseObject<List<TaskResponseViewModel>> GetTasks(string id)
        {
            ResponseObject<List<TaskResponseViewModel>> response = new ResponseObject<List<TaskResponseViewModel>>();

            var user = db.Users.FirstOrDefault(i => i.Username == id);

            if (user == null)
            {
                response.ErrorMessage = "Kullanıcı bulunamadı.";
                response.IsSuccess = false;
                return response;
            }

            var tasks = db.Tasks.Where(i => i.Owner.ID == user.ID).ToList();

            TaskResponseViewModel viewModel;

            List<TaskResponseViewModel> list = new List<TaskResponseViewModel>();

            foreach (var item in tasks)
            {
                viewModel = new TaskResponseViewModel()
                {
                    ID=item.ID,
                    CreatedOn = item.CreatedOn,
                    DueDate = item.DueDate,
                    IsCompleted = item.IsComleted,
                    ModifiedOn = item.ModifiedOn,
                    Text = item.Text,
                    Username = item.Owner.Username
                };
                list.Add(viewModel);

            }

            response.Result = list;
            return response;

        }


        public ResponseObject<TaskResponseViewModel> GetTaskById(int id)
        {
            ResponseObject<TaskResponseViewModel> response = new ResponseObject<TaskResponseViewModel>();


            var task = db.Tasks.FirstOrDefault(i => i.ID == id);
            if (task == null)
            {
                response.ErrorMessage = "Böyle bir görev bulunamadı.";
                response.IsSuccess = false;
                return response;
            }

            TaskResponseViewModel viewModel = new TaskResponseViewModel
            {
                ID=task.ID,
                Text = task.Text,
                CreatedOn = task.CreatedOn,
                DueDate = task.DueDate,
                IsCompleted = task.IsComleted,
                ModifiedOn = task.ModifiedOn,
                Username = task.Owner.Username
            };

            response.Result = viewModel;
            response.IsSuccess = true;
            return response;

        }



        [HttpPost]
        public ResponseObject<TaskResponseViewModel> CreateTask(TaskRequestViewModel model)
        {
            ResponseObject<TaskResponseViewModel> response = new ResponseObject<TaskResponseViewModel>();

            if (ModelState.IsValid)
            {
                var task = new Task();
                task.Owner = db.Users.FirstOrDefault(i => i.Username == model.Username);
                task.Text = model.Text;
                task.IsComleted = false;
                task.CreatedOn = DateTime.Now;
                db.Tasks.Add(task);

                if (db.SaveChanges() > 0)
                {
                    response.IsSuccess = true;
                    
                    return response;
                }

                response.IsSuccess = false;

                return response;

            }

            return response;
        }


        [HttpPut]
        public ResponseObject<TaskResponseViewModel> UpdateTask(TaskRequestViewModel model)
        {
            ResponseObject<TaskResponseViewModel> response = new ResponseObject<TaskResponseViewModel>();

            if (ModelState.IsValid)
            {
                var task = db.Tasks.FirstOrDefault(i => i.ID == model.ID);
                task.Owner = db.Users.FirstOrDefault(i => i.Username == model.Username);
                task.Text = model.Text;
                task.IsComleted = model.IsCompleted;
                task.ModifiedOn = DateTime.Now;
                if (task.IsComleted)
                {
                    task.DueDate = DateTime.Now;
                }

                db.Entry(task).State = System.Data.Entity.EntityState.Modified;

                if (db.SaveChanges() > 0)
                {
                    response.IsSuccess = true;
                    return response;
                }

                response.IsSuccess = false;

                return response;

            }

            return response;
        }


        [HttpDelete]

        public ResponseObject<TaskResponseViewModel> DeleteTask(int id)
        {
            ResponseObject<TaskResponseViewModel> response = new ResponseObject<TaskResponseViewModel>();


            var task = db.Tasks.FirstOrDefault(i => i.ID == id);
            if (task == null)
            {
                response.ErrorMessage = "Böyle bir görev bulunamadı.";
                response.IsSuccess = false;
                return response;
            }

            db.Tasks.Remove(task);
            if (db.SaveChanges() > 0)
            {
                response.IsSuccess = true;
                response.SuccessMessage = "Silme işlemi başarıyla gerçekleşti.";
                return response;
            }

            response.IsSuccess = false;
            response.ErrorMessage = "Hata oluştu.";
            return response;
        }
    }
}
