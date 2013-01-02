using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTracker.DAL;
using TimeTracker.Models;

namespace TimeTracker.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Active()
        {
            List<User> users = UserUtility.GetAllActiveUsers();
            List<UserViewModel> viewModel = new List<UserViewModel>(users.Count());
            foreach (User item in users)
            {
                viewModel.Add(new UserViewModel(item));
            }
            return View(viewModel);
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id)
        {
            User user = UserUtility.GetUserById(id);
            UserViewModel userViewModel = new UserViewModel(user);
            return View(userViewModel);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {


                UserViewModel user = new UserViewModel();
                user.UserName = collection["UserName"];
                user.FirstName = collection["FirstName"];
                user.LastName = collection["LastName"];
                user.Position = collection["Position"];
                user.Email = collection["Email"];

                UserUtility.UpdateUser(id, user.UserName, user.FirstName, user.LastName, user.Position, user.Email);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            User usr = UserUtility.GetUserById(id);
            return View(usr);
        }

        public ActionResult Delete(int id)
        {
            UserUtility.DeleteUserById(id);

            return View("Index");
        }
    }
}
