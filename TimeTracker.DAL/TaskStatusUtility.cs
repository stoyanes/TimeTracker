using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL
{
    public static class TaskStatusUtility
    {
        public static string GetStatMessById(int id)
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                var mess = (from taskStat in context.TasksStatus
                            where taskStat.Id == id
                            select taskStat.Status).FirstOrDefault<string>();
                return mess;
            }
        }
    }
}
