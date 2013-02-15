using System;
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
                                 where usrTasks.UserID == usr.UserId && usrTasks.Task.IsDeleted == false
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
                           where usrTsk.TaskId == tskId && usrTsk.UserID == usrId
                           select usrTsk).FirstOrDefault<UsersTask>();
                context.UsersTasks.Attach(usrTask);
                if (!usrTask.WorkedHours.HasValue)
                {
                    usrTask.WorkedHours = 0;
                }
                if (hours > 0)
                {
                    usrTask.WorkedHours += hours;
                }

                context.SaveChanges();
            }
        }

        public static List<UsersTask> GetAllUsersOnTask(int taskId)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                List<UsersTask> result = (from usrTsk in context.UsersTasks
                                          where usrTsk.TaskId == taskId
                                          select usrTsk).ToList<UsersTask>();

                return result;
            }
        }


        public static void UpdateUsersTaskDate(int taskId, DateTime date)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                List<UsersTask> tasksToUpdate = (from usrTask in context.UsersTasks
                                                 where usrTask.TaskId == taskId
                                                 select usrTask).ToList<UsersTask>();

                for (int i = 0; i < tasksToUpdate.Count(); i++)
                {
                    tasksToUpdate[i].StartDate = date;
                }

                context.SaveChanges();
            }
        }

        
    }
}
