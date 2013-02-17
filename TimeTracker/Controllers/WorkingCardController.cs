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
            WorkingCard wCard = WorkingCardUtility.GetWorkingCardById(id);
            return View(wCard);
        }


        //
        // GET: /WorkingCard/Edit/5

        public ActionResult Edit(int id)
        {
            WorkingCard wCard = WorkingCardUtility.GetWorkingCardById(id);

            Task tsk = TaskUtility.GetTaskById(wCard.TaskId);

            WorkingCardModel resultCard = new WorkingCardModel(wCard.Id, wCard.StartDate, wCard.WorkingHours, wCard.Description, wCard.IsFilled.Value, tsk.Title);


            return View(resultCard);
        }


        //
        // POST: /WorkingCard/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            WorkingCard wCard = new WorkingCard();
            wCard.Id = id;
            wCard.StartDate = DateTime.Parse(collection["StartDate"]);
            wCard.Description = collection["Description"];
            wCard.WorkingHours = TimeSpan.Parse(collection["WorkingHours"]);

            WorkingCardUtility.UpdateWoringCard(wCard);
            return RedirectToAction("Active", "Users");


        }

        public ActionResult ShowAll(int id)
        {

            List<WorkingCard> allCards = WorkingCardUtility.GetAllByUserName(id);

            List<WorkingCardModel> resultCards = new List<WorkingCardModel>(allCards.Count());

            Task tsk = new Task();
            foreach (WorkingCard item in allCards)
            {
                tsk = TaskUtility.GetTaskById(item.TaskId);
                resultCards.Add(new WorkingCardModel(item.Id, item.StartDate, item.WorkingHours, item.Description, item.IsFilled.Value, tsk.Title));
            }


            return View(resultCards);

        }

        // id - task id
        public ActionResult FillWorkingCard(int id)
        {

            return View();
        }

        [HttpPost]
        // id - task id
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


            // This portion code is for simple computing of working hours. Due to my stupid implementation in database
            // where hours is int/float I just add 1 hour when minutes are >= 30

            int tmpMins = 0;
            if (workingCard.WorkingHours.Minutes >= 30)
            {
                tmpMins += 1;
            }

            workingCard.Description = collection["Description"];
            workingCard.IsFilled = true;

            WorkingCardUtility.AddCardToDb(usr.UserId, id, workingCard.StartDate, workingCard.WorkingHours, workingCard.Description, workingCard.IsFilled);

            UserTasksUtility.UpdateUserWorkingHoursOnTask(usr.UserId, id, workingCard.WorkingHours.Hours + tmpMins);
            TaskUtility.UpdateWorkingHoursOnTask(id, workingCard.WorkingHours.Hours + tmpMins);

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
            StringBuilder message = new StringBuilder();
            message.AppendLine(DateTime.Now.ToString());
            message.AppendLine(text);
            message.AppendLine("=========================================");

            System.IO.File.AppendAllText(logFile, message.ToString());
        }
    }
}
