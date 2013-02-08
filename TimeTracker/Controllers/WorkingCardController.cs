using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult ShowAll()
        {
            if (User.Identity.IsAuthenticated)
            {
                List<WorkingCard> allCard = WorkingCardUtility.GetAllByUserName(User.Identity.Name);
                return View(allCard);

            }
            
            return View();
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
            // TODO validation logic
            workingCard.WorkingHours = TimeSpan.Parse(collection["WorkingHours"]);

            workingCard.Description = collection["Description"];
            workingCard.IsFilled = true;

            WorkingCardUtility.AddCardToDb(usr.UserId, id, workingCard.StartDate, workingCard.WorkingHours, workingCard.Description, workingCard.IsFilled);

            UserTasksUtility.UpdateUserWorkingHoursOnTask(usr.UserId, id, workingCard.WorkingHours.Hours);
            return RedirectToAction("Index");

        }
    }
}
