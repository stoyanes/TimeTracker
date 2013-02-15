using System;
using System.Linq;

namespace TimeTracker.Models
{
    public class WorkingCardViewModel : WorkingCardModel
    {
        public string TaskTitle { get; set; }

        public WorkingCardViewModel()
            : base()
        {
            TaskTitle = String.Empty;
        }

        public WorkingCardViewModel(int cardId, DateTime sd, TimeSpan wh, string d, bool isF, string taskTitle)
            : base(cardId, sd, wh, d, isF)
        {
            TaskTitle = taskTitle;
        }


    }
}