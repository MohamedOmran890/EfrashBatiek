using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public enum Zone : short
    {
        Qena=1,
        Cairo,
        Assiut,
        Sohag,
        Minya,
        Aswan,
        Luxor
    }
    public class Address
    {
        public int ID { get; set; }
        
        public string FirstName { get; set; }
       
        public string LastName { get; set; }
      
        public string phone { get; set; }
     
        public int PostalCode { get; set; }
        public bool SetDefault { get; set; }
      
        public Zone Zone { get; set; }
    

      
        public string FullAddress { get; set; }
       
        [ForeignKey("User")]
        public string UserId {get;set;}
        public virtual User User { get; set; }
        public string description { get; set; } 
    }
}
