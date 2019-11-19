using Ertglr.Libraries.Common.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoAppWebApi.BLL;
using TodoAppWebApi.DAL.Context;
using TodoAppWebApi.ENTITIES.EntityClass;
using TodoAppWebApi.ENTITIES.ViewModels;

namespace TodoAppWebApi.Controllers
{
    public class UserController : ApiController
    {
        TodoContext db = null;
        public UserController()
        {
            db = new TodoContext();
        }

        [HttpPost]
        public ResponseObject<LoginResponseViewModel> Authenticate(LoginRequestViewModel model)
        {
            var user = db.Users.FirstOrDefault(i => i.Email == model.Email && i.Password == model.Password);
            if (user == null)
            {
                return null;
            }
            LoginResponseViewModel us = new LoginResponseViewModel();
            us.Firstname = user.Firstname;
            us.Lastname = user.Lastname;
            us.Username = user.Username;

            ResponseObject<LoginResponseViewModel> response = new ResponseObject<LoginResponseViewModel>();
            response.Result = us;
            response.IsSuccess = true;

            return response;
        }


        public ResponseObject<RegisterRequestViewModel> GetUserById(Guid id)
        {
            try
            {


                var user = db.Users.FirstOrDefault(i => i.ID==id);
                if (user == null)
                {
                    return null;
                }

                RegisterRequestViewModel viewModel = new RegisterRequestViewModel
                {
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Username = user.Username,
                    
                };

                ResponseObject<RegisterRequestViewModel> response = new ResponseObject<RegisterRequestViewModel>();
                response.Result = viewModel;
                response.IsSuccess = true;

                return response;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [HttpGet]
        public ResponseObject<List<LoginResponseViewModel>> GetUsers()
        {


            List<LoginResponseViewModel> users = new List<LoginResponseViewModel>();

            foreach (var item in db.Users.ToList())
            {
                users.Add(new LoginResponseViewModel()
                {
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Username = item.Username,
                    Email = item.Email
                });
            }

            ResponseObject<List<LoginResponseViewModel>> response = new ResponseObject<List<LoginResponseViewModel>>();
            response.Result = users;
            response.IsSuccess = true;


            return response;
        }

        public ResponseObject<RegisterResponseViewModel> PostUser(RegisterRequestViewModel model)
        {
            try
            {
                ResponseObject<RegisterResponseViewModel> response = new ResponseObject<RegisterResponseViewModel>();

                if (ModelState.IsValid)
                {

                    TodoAppWebApi.ENTITIES.EntityClass.User user = new TodoAppWebApi.ENTITIES.EntityClass.User();
                    user.Firstname = model.Firstname;
                    user.Lastname = model.Lastname;
                    user.Username = model.Username;

                    user.Email = model.Email;
                    user.CreatedOn = DateTime.Now;
                    user.Password = model.Password;
                    user.ConfirmPassword = model.ConfirmPassword;
                    db.Users.Add(user);
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
            catch (Exception e)
            {

                throw e;
            }
        }


        //    [HttpPut]
        //    public IHttpActionResult PutUser(User userVm)
        //    {
        //        try
        //        {
        //            //var user = db.Users.Find(userVm.ID);
        //            if (ModelState.IsValid)
        //            {

        //                //user.ConfirmPassword = userVm.ConfirmPassword;
        //                userVm.ModifiedOn = DateTime.Now;

        //                db.Entry(userVm).State = System.Data.Entity.EntityState.Modified;
        //                if (db.SaveChanges() > 0)
        //                {
        //                    return GetUserById(userVm.Username);
        //                }
        //                return BadRequest();
        //            }


        //            return BadRequest();

        //        }
        //        catch (Exception e)
        //        {

        //            throw e;
        //        }
        //    }

    }
}
