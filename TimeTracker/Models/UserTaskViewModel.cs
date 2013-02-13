using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTracker.Models
{
    public class UserTaskViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public int WorkedHoursCurrent { get; set; }


        public UserTaskViewModel(int usrId, string usrName, string firstName, string lastName, int tskId, string title, int hours)
        {
            UserId = usrId;
            UserName = usrName;
            FirstName = firstName;
            LastName = lastName;
            TaskId = tskId;
            Title = title;
            WorkedHoursCurrent = hours;
        }
    }
}