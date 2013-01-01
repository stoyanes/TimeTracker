using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeTracker.DAL;

namespace TimeTracker.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public int WorkedHoursCurrent { get; set; }
        public int StatusId { get; set; }
        public string StatusMess { get; set; }

        public TaskViewModel(Task task, string statusMess)
        {
            Id = task.Id;
            Title = task.Title;
            Description = task.Description;
            StartDate = task.StartDate;
            WorkedHoursCurrent = task.WorkedHoursCurrent;
            StatusId = task.StatusId;
            StatusMess = statusMess;
        }

        public TaskViewModel(int id, string t, string d, int whc, int si, DateTime? sd = null)
        {
            Id = id;
            Title = t;
            Description = d;
            StartDate = sd;
            WorkedHoursCurrent = whc;
            StatusId = si;
        }

    }
}