using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TimeTracker.Models
{
    public class WorkingCardModel
    {
        public DateTime? StartDate { get; set; }

        [Required]
        public TimeSpan WorkingHours { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public bool IsFilled { get; set; }

        public WorkingCardModel()
        {
            StartDate = DateTime.Now;
            WorkingHours = new TimeSpan();
            Description = string.Empty;
            IsFilled = false;
        }

        public WorkingCardModel(DateTime sd, TimeSpan wh, string d, bool isF)
        {
            StartDate = sd;
            WorkingHours = wh;
            Description = d;
            IsFilled = isF;
        }
    }
}