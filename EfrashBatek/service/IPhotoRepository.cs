using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface IPhotoRepository
    {
        void Create(Photo photo);
        int Delete(int Id);
        List<Photo> GetAll();
        Photo GetById(int Id);
        int Update(int id, Photo photo);
    }
}