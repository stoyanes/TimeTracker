using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL
{
    public static class UserTasksUtility
    {
        public static List<Task> GetAllUserTasks(string userName)
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                var tasks = (from task in context.Tasks
                             where task.Users.Any(name => name.UserName == userName)
                             select task).ToList();

                return tasks;
                            
            }
        }
    }
}
