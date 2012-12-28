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

        public static User GetUserById(int id)
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                var user = (from usr in context.Users
                            where usr.UserId == id
                            select usr).FirstOrDefault<User>();
                return user;
            }
        }

        public static void DeleteUserById(int id)
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                var user = (from usr in context.Users
                            where usr.UserId == id
                            select usr).FirstOrDefault<User>();
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
        }
    }
}
