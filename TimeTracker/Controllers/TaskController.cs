using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeTracker.DAL;
using TimeTracker.Models;

namespace TimeTracker.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Task/Details/5

        public ActionResult Details(int id)
        {
            Task task = TaskUtility.GetTaskById(id);
            return View();
        }

        //
        // GET: /Task/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                TaskModel task = new TaskModel();
                task.Title = collection["Title"];
                task.Description = collection["Description"];
                task.StatusId = int.Parse(collection["StatusId"]);
                if (collection["StartDate"] != null)
                    task.StartDate = DateTime.Parse(collection["StartDate"]);
                if (collection["EndDate"] != null)
                    task.EndDate = DateTime.Parse(collection["EndDate"]);

                TaskUtility.CreateTask(task.Title, task.Description, task.StatusId, task.StartDate, task.EndDate);
                // TaskUtility.CreateTask(task.Title, task.Description, task.StatusId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Task/Edit/5

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
        // GET: /Task/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Task/Delete/5

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

        public ActionResult Active()
        {
            List<Task> tasks = TaskUtility.GetAllActiveTasks();
            List<TaskViewModel> viewModel = new List<TaskViewModel>(tasks.Count());
            string message = null;
            foreach (Task item in tasks)
            {
                message = TaskStatusUtility.GetStatMessById(item.Id);
                viewModel.Add(new TaskViewModel(item, message));
            }
            return View(viewModel);
        }
    }
}
