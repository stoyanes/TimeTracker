using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using TimeTracker.DAL;
using TimeTracker.Models;

namespace TimeTracker.Controllers
{
    public class WorkingCardController : Controller
    {
        //
        // GET: /WorkingCard/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /WorkingCard/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /WorkingCard/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /WorkingCard/Create

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
        // GET: /WorkingCard/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /WorkingCard/Edit/5

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
        // GET: /WorkingCard/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /WorkingCard/Delete/5

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

        public ActionResult ShowAll(int id)
        {

            List<WorkingCard> allCards = WorkingCardUtility.GetAllByUserName(id);

            List<WorkingCardViewModel> resultCards = new List<WorkingCardViewModel>(allCards.Count());

            Task tsk = new Task();
            foreach (WorkingCard item in allCards)
            {
                tsk = TaskUtility.GetTaskById(item.TaskId);
                //WorkingCardViewModel(DateTime sd, TimeSpan wh, string d, bool isF, string taskTitle)
                resultCards.Add(new WorkingCardViewModel(item.Id, item.StartDate, item.WorkingHours, item.Description, item.IsFilled.Value, tsk.Title));
            }


            return View(resultCards);

        }


        public ActionResult FillWorkingCard(int id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult FillWorkingCard(int id, FormCollection collection)
        {

            User usr = UserUtility.GetUserByName(User.Identity.Name);

            WorkingCardModel workingCard = new WorkingCardModel();
            try
            {
                workingCard.StartDate = DateTime.Parse(collection["StartDate"]);
            }
            catch (FormatException)
            {
                workingCard.StartDate = DateTime.Now;
            }
            workingCard.WorkingHours = TimeSpan.Parse(collection["WorkingHours"]);

            workingCard.Description = collection["Description"];
            workingCard.IsFilled = true;

            WorkingCardUtility.AddCardToDb(usr.UserId, id, workingCard.StartDate, workingCard.WorkingHours, workingCard.Description, workingCard.IsFilled);

            UserTasksUtility.UpdateUserWorkingHoursOnTask(usr.UserId, id, workingCard.WorkingHours.Hours);
            TaskUtility.UpdateWorkingHoursOnTask(id, workingCard.WorkingHours.Hours);

            return RedirectToAction("Current", "Task");
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
