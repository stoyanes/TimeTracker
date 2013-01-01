using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL
{
    public static class TaskUtility
    {
        public static void CreateTask(Task task)
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                context.Tasks.Add(task);
                context.SaveChanges();
            }
        }
        public static void CreateTask(string t, string d, int s, DateTime? sd = null, DateTime? ed = null)
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                Task task = new Task();
                task.Title = t;
                task.Description = d;
                task.StatusId = s;
                task.StartDate = sd;
                task.EndDate = ed;
                context.Tasks.Add(task);
                context.SaveChanges();
            }
        }

        public static List<Task> GetAllActiveTasks()
        {
            using (TimeTrackerDbEntities1  context = new TimeTrackerDbEntities1())
            {
                var tasks = (from task in context.Tasks
                             where task.IsDeleted == false
                             select task).ToList<Task>();
                return tasks;
            }
        }

        public static Task GetTaskById(int id)
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                var resultTask = (from task in context.Tasks
                                  where task.Id == id
                                  select task).FirstOrDefault<Task>();
                return resultTask;
            }
        }
    }
}
