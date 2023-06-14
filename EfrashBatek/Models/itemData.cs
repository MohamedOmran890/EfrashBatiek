using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace EfrashBatek.Models
{
    public class itemData
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public string image { get; set; }   
        public int price { get; set; }
        public string description { get; set; }
       
        public int itemid { get; set; } 
       
    }
}