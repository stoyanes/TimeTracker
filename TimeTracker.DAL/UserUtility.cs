using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.DAL
{
    public static class UserUtility
    {
        public static List<User> GetAllActiveUsers()
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                var users = (from usr in context.Users
                             where usr.IsDeleted == false
                             select usr).ToList<User>();
                return users;
            }
        }

        public static User GetUserById(int id)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                var user = (from usr in context.Users
                            where usr.UserId == id
                            select usr).FirstOrDefault<User>();
                return user;
            }
        }

        public static bool IsUserActive(string userName)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
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
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                var user = GetUserById(id);
                context.Users.Attach(user);
                if (user != null)
                {
                    user.IsDeleted = true;
                    context.SaveChanges();
                }
            }
        }

        public static void UpdateUser(int id, string un, string fn, string ln, string p, string e)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                var user = GetUserById(id);
                context.Users.Attach(user);
                if (user != null)
                {
                    user.UserName = un;
                    user.FirstName = fn;
                    user.LastName = ln;
                    user.Position = p;
                    user.Email = e;
                    context.SaveChanges();
                }
            }
        }

        public static User GetUserByName(string userName)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                var userRes = (from usr in context.Users
                               where usr.UserName == userName
                               select usr).FirstOrDefault<User>();
                return userRes;
            }

        }

        public static List<Task> GetAllUserTasks(string id)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                User user = GetUserByName(id);
                context.Users.Attach(user);
                if (user != null)
                {
                    var tasks = user.UsersTasks.Select(u => u.Task);
                    return tasks.ToList();
                }
                else
                    throw new Exception("Error!");

            }
        }

        public static List<User> GetAllUsersNotInTask(int taskId)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                string query = @"select *
                                from [TimeTrackerDb].[dbo].[Users] as usr
                                where usr.IsDeleted = 0 and 
                                    usr.UserId not in (select usrTask.UserID
						                               from [TimeTrackerDb].[dbo].[UsersTasks] as usrTask
						                                where usrTask.TaskId = {0}
						                                )";

                object[] parameters = { taskId };
                var result = (context as IObjectContextAdapter).ObjectContext.ExecuteStoreQuery<User>(query, parameters);
                return result.ToList<User>();
            }

        }
    }
}
