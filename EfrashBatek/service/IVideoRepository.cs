using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IVideoRepository
    {
        void Create(Video video);
        int Delete(int Id);
        List<Video> GetAll();
        Video GetById(int Id);
        int Update(int id, Video video);
    }
}