using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TimeTracker.DAL;
using TimeTracker.Models;



namespace TimeTracker.Controllers
{
    public class TaskController : Controller
    {
        //
        // GET: /Task/

        public ViewResult Index()
        {
            return View();
        }

        //
        // GET: /Task/Details/5

        public ViewResult Details(int id)
        {
            List<UsersTask> usrTsk = UserTasksUtility.GetAllUsersOnTask(id);
            List<UserTaskViewModel> usrTskViewModel = new List<UserTaskViewModel>(usrTsk.Count);

            User usr = new User();
            Task task = new Task();

            foreach (UsersTask item in usrTsk)
            {
                usr = UserUtility.GetUserById(item.UserID);
                task = TaskUtility.GetTaskById(item.TaskId);

                if (item.WorkedHours.HasValue)
                {
                    usrTskViewModel.Add(new UserTaskViewModel(usr.UserId, usr.UserName, usr.FirstName, usr.LastName, task.Id, task.Title, (int)item.WorkedHours.Value));
                }
                else
                {
                    usrTskViewModel.Add(new UserTaskViewModel(usr.UserId, usr.UserName, usr.FirstName, usr.LastName, task.Id, task.Title, 0));
                }

            }

            return View(usrTskViewModel);
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
            TaskModel task = new TaskModel();
            task.Title = collection["Title"];
            task.Description = collection["Description"];
            task.StatusId = int.Parse(collection["StatusId"]);
            try
            {
                if (collection["StartDate"] != null)
                    task.StartDate = DateTime.Parse(collection["StartDate"]);
            }
            catch (FormatException)
            {
                task.StartDate = DateTime.Today;
            }
            if (collection["EndDate"] != null)
                task.EndDate = DateTime.Parse(collection["EndDate"]);

            TaskUtility.CreateTask(task.Title, task.Description, task.StatusId, task.StartDate, task.EndDate);


            return RedirectToAction("Active");
        }



        //
        // GET: /Task/Edit/5

        public ActionResult Edit(int id)
        {
            Task task = TaskUtility.GetTaskById(id);
            TaskViewModel taskViewModel = new TaskViewModel(task);
            return View(taskViewModel);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            TaskModel task = new TaskModel();
            task.Title = collection["Title"];
            task.Description = collection["Description"];
            task.StatusId = int.Parse(collection["StatusId"]);

            if (collection["StartDate"] != null)
            {
                task.StartDate = DateTime.Parse(collection["StartDate"]);
                UserTasksUtility.UpdateUsersTaskDate(id, task.StartDate.Value);
            }

            TaskUtility.UpdateTask(id, task.Title, task.Description, task.StatusId, task.StartDate);


            return RedirectToAction("Active");


        }

        //
        // GET: /Task/Delete/5

        public ActionResult Delete(int id)
        {
            TaskUtility.DeleteTaskById(id);
            return View("Index");
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
        [Authorize(Roles = "Admin")]
        public ActionResult Active()
        {

            List<Task> tasks = TaskUtility.GetAllActiveTasks();
            List<TaskViewModel> viewModel = new List<TaskViewModel>(tasks.Count());
            string message = null;
            foreach (Task item in tasks)
            {
                message = TaskStatusUtility.GetStatMessById(item.StatusId);
                viewModel.Add(new TaskViewModel(item, message));
            }
            return View(viewModel);
        }

        [Authorize(Roles = "Admin, RegularUser")]
        public ActionResult Current()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<UsersTask> userTasks = UserTasksUtility.GetAllUserTasks(User.Identity.Name);

                return View(userTasks);
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult ShowUsersToAdd(int id)
        {

            List<User> usrList = UserUtility.GetAllUsersNotInTask(id);
            Task task = TaskUtility.GetTaskById(id);
            ViewBag.TaskTitle = task.Title;
            ViewBag.TaskId = task.Id;
            return View(usrList);
        }


        [HttpGet]
        public ActionResult AddUserToTask(int id)
        {
            List<User> usrList = UserUtility.GetAllUsersNotInTask(id);
            Task tsk = TaskUtility.GetTaskById(id);
            ViewBag.TaskId = tsk.Id;
            ViewBag.TaskTitle = tsk.Title;

            return View(usrList);
        }

        [HttpPost]
        public ActionResult AddUserToTask(int id, FormCollection collection)
        {
            int taskToUser = int.Parse(collection["TaskToUser"]);
            UserTasksUtility.AddTaskToUser(taskToUser, id);

            return RedirectToAction("Active");

        }

        protected override void OnException(ExceptionContext filterContext)
        {
            string logErrorFile = HttpContext.Server.MapPath("~/App_Data/LogErrorFile.txt");
            WriteLog(logErrorFile, filterContext.Exception.ToString());

            if (!filterContext.HttpContext.IsCustomErrorEnabled)
            {
                filterContext.ExceptionHandled = true;
                this.View("Error").ExecuteResult(this.ControllerContext);
            }
        }

        static void WriteLog(string logFile, string text)
        {
            //TODO: Format nicer
            StringBuilder message = new StringBuilder();
            message.AppendLine(DateTime.Now.ToString());
            message.AppendLine(text);
            message.AppendLine("=========================================");

            System.IO.File.AppendAllText(logFile, message.ToString());
        }

    }
}
