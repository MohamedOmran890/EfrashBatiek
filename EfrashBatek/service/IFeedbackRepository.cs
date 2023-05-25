using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IFeedbackRepository
    {
        void Create(Feedback feedback);
        int Delete(int Id);
        List<Feedback> GetAll();
        Feedback GetById(int Id);
        int Update(int id, Feedback feedback);
    }
}