using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTracker.DAL;
using TimeTracker.Models;

namespace TimeTracker.Controllers
{
    public class AdministratorController : Controller
    {
        //
        // GET: /Administrator/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowAllUsers()
        {
            List<User> users = UserUtility.GetAllUsers();
            List<UserViewModel> viewModel = new List<UserViewModel>(users.Count());
            foreach (User item in users)
            {
                viewModel.Add(new UserViewModel(item));
            }
            return View(viewModel);
        }

        public ActionResult Edit()
        {
            return RedirectToAction("Manage", "Account");
        }

    }
}
