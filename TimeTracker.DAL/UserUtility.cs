using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL
{
    public static class UserUtility
    {
        public static List<User> GetAllActiveUsers()
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                var users = (from usr in context.Users
                             where usr.IsDeleted == false
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

        public static bool IsUserActive(string userName)
        {
            using (TimeTrackerDbEntities1 context = new TimeTrackerDbEntities1())
            {
                var user = (from usr in context.Users
                        where usr.UserName == userName
                        select usr).FirstOrDefault<User>();
                if (user.IsDeleted == true)
                    return false;
            }
            return true;
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
                    user.IsDeleted = true;
                    context.SaveChanges();
                }
            }
        }
    }
}
