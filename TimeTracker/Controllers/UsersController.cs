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

        public ActionResult Edit(int id)
        {
            // TODO tobe implemented.
            return View();
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
