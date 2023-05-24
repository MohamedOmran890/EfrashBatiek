using EfrashBatek.Models;
using System.Collections.Generic;
using System.Linq;

namespace EfrashBatek.service
{
    public class VideoRepository : IVideoRepository
    {
        Context context;
        public VideoRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Video video)
        {
            context.Videos.Add(video);

        }
        public int Update(int id, Video video)
        {
            var ans = context.Videos.FirstOrDefault(x => x.ID == id);
            ans.Name = video.Name;
            ans.URL = video.URL;
            ans.ItemID = video.ItemID;
            ans.Description = video.Description;
            context.Videos.Update(ans);
            int num = context.SaveChanges();
            return num;
        }
        public int Delete(int Id)
        {
            var ans = context.Videos.FirstOrDefault(x => x.ID == Id);
            context.Videos.Remove(ans);
            int num = context.SaveChanges();
            return num;
        }
        public Video GetById(int Id)
        {
            var ans = context.Videos.FirstOrDefault(x => x.ID == Id);
            return ans;
        }
        public List<Video> GetAll()
        {
            var ans = context.Videos.ToList();
            return ans;
        }
    }
}
