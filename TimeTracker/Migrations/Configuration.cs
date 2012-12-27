namespace TimeTracker.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using WebMatrix.WebData;

    internal sealed class Configuration : DbMigrationsConfiguration<TimeTracker.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TimeTracker.Models.UsersContext context)
        {
            //TODO here we have dublicated code
            //WebSecurity.InitializeDatabaseConnection(
            //       connectionStringName: "DefaultConnection",
            //       userTableName: "Users",
            //       userIdColumn: "UserId",
            //       userNameColumn: "UserName",
            //       autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Administrator"))
            {
                roles.CreateRole("Administrator");
            }
            if (membership.GetUser("admin", false) == null)
            {
                membership.CreateUserAndAccount("admin", "00000000");
            }
            if (!roles.GetRolesForUser("admin").Contains("Administrator"))
            {
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "Administrator" });
            }
        }
    }
}
