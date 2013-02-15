using System;
using System.Collections.Generic;
using System.Linq;


namespace TimeTracker.DAL
{
    public static class WorkingCardUtility
    {
        public static void AddCardToDb(int usrId, int taskId, DateTime? startDate, TimeSpan hoursMin, string description, bool isFilled)
        {
            
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                WorkingCard wCard = new WorkingCard();
                wCard.UserId = usrId;
                wCard.TaskId = taskId;
                wCard.StartDate = DateTime.Parse(startDate.Value.ToString());
                wCard.WorkingHours = hoursMin;
                wCard.Description = description;
                wCard.IsFilled = isFilled;

                context.WorkingCard.Add(wCard);
                context.SaveChanges();
            }
        }


        public static List<WorkingCard> GetAllByUserName(int id)
        {
            using (TimeTrackerDbEntities context = new TimeTrackerDbEntities())
            {
                User usr = UserUtility.GetUserById(id);
                List<WorkingCard> allCards = (from card in context.WorkingCard
                                              where card.UserId == usr.UserId
                                              select card).ToList<WorkingCard>();
                return allCards;
            }
        }
        
    }
}
