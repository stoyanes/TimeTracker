using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL
{
    public static class UserUtility
    {
        public static List<User> GetAllUsers()
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                var users = (from usr in context.Users
                             select usr).ToList<User>();
                return users;
            }
        }
    }
}
