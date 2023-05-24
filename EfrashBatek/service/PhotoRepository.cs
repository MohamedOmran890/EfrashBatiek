using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class PhotoRepository : IPhotoRepository
    {
        Context context;
        public PhotoRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Photo photo)
        {
            context.Photos.Add(photo);

        }
        public int Update(int id, Photo photo)
        {
            var ans = context.Photos.FirstOrDefault(x => x.ID == id);
            ans.DesignID = photo.DesignID;
            ans.Image = photo.Image;
            context.Photos.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Photos.FirstOrDefault(x => x.ID == Id);
            context.Photos.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Photo GetById(int Id)
        {
            var ans = context.Photos.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Photo> GetAll()
        {
            var ans = context.Photos.ToList();
            return ans;
        }
    }
}
