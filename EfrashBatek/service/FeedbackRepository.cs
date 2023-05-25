using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class FeedbackRepository : IFeedbackRepository
    {
        Context context;
        public FeedbackRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Feedback feedback)
        {
            context.feedbacks.Add(feedback);

        }
        public int Update(int id, Feedback feedback)
        {
            var ans = context.feedbacks.FirstOrDefault(x => x.ID == id);
            ans.ComplaintMessage = feedback.ComplaintMessage;
            context.feedbacks.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.feedbacks.FirstOrDefault(x => x.ID == Id);
            context.feedbacks.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Feedback GetById(int Id)
        {
            var ans = context.feedbacks.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Feedback> GetAll()
        {
            var ans = context.feedbacks.ToList();
            return ans;
        }
    }
}
