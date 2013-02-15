using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TimeTracker.Models
{
    public class WorkingCardModel
    {
        public int wCardId { get; set; }
        public DateTime? StartDate { get; set; }

        [Required]
        [RegularExpression(@"^(?:0?[0-9]|1[0-4]):[0-5][0-9]$", ErrorMessage = "Invalid time. Note: The correct format is HH:MM, where HH is for hours, MM is for minutes. Example: 7:45")]
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

        public WorkingCardModel(int cardId, DateTime sd, TimeSpan wh, string d, bool isF)
        {
            wCardId = cardId;
            StartDate = sd;
            WorkingHours = wh;
            Description = d;
            IsFilled = isF;
        }
    }
}