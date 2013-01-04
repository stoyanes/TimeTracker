using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTracker.Controllers
{
    public class UserTasksController : Controller
    {
        //
        // GET: /UserTasks/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /UserTasks/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /UserTasks/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserTasks/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserTasks/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /UserTasks/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserTasks/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UserTasks/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
