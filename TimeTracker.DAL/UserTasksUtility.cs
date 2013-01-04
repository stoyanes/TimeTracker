﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
