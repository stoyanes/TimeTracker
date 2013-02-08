﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace TimeTracker.DAL
{
    public static class UserTasksUtility
    {
        public static void AddTaskToUser(int userId, int taskId, DateTime? startDate = null, int? workedHours = null, DateTime? endDate = null)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                UsersTask usrTask = new UsersTask();
                usrTask.UserID = userId;
                usrTask.TaskId = taskId;
                usrTask.StartDate = startDate;
                usrTask.WorkedHours = workedHours;
                usrTask.EndDate = endDate;

                context.UsersTasks.Add(usrTask);
                context.SaveChanges();
            }
        }

        public static List<UsersTask> GetAllUserTasks(string userName)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                User usr = UserUtility.GetUserByName(userName);
                var UserTasks = (from usrTasks in context.UsersTasks.Include("Task")
                                 where usrTasks.UserID == usr.UserId
                                 select usrTasks).ToList<UsersTask>();
                return UserTasks;

                
            }
        }

        public static void UpdateUserWorkingHoursOnTask(int usrId, int tskId, double hours)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                UsersTask usrTask = new UsersTask();
                usrTask = (from usrTsk in context.UsersTasks
                           where usrTsk.TaskId == tskId && usrTsk.UserID == usrTsk.UserID
                           select usrTsk).FirstOrDefault<UsersTask>();
                context.UsersTasks.Attach(usrTask);
                if (usrTask.WorkedHours == null)
                {
                    usrTask.WorkedHours = 0;
                }
                if (hours > 0)
                    usrTask.WorkedHours += hours;
                context.SaveChanges();
            }
        }
    }
}