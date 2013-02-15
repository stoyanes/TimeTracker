using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TimeTracker.DAL;

namespace TimeTracker.Models
{
    public class TaskModel
    {

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Description")]
        public string  Description { get; set; }
        
        public int StatusId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public TaskModel(Task task)
        {
            Title = task.Title;
            Description = task.Description;
            StatusId = task.StatusId;
        }
        public TaskModel(string t, string d, int s)
        {
            Title = t;
            Description = d;
            StatusId = s;
        }

        public TaskModel(string t, string d, int s, DateTime? sd = null, DateTime? ed = null)
        {
            Title = t;
            Description = d;
            StatusId = s;
            StartDate = sd;
            EndDate = ed;
        }

        public TaskModel()
        {
            Title = null;
            Description = "Some description";
            StatusId = 0;
        }
    }
}