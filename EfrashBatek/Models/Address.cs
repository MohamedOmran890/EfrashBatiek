using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public enum Zone:short
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
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public int PostalCode { get; set; }
        public bool SetDefault { get; set; }
        [Required]
        public Zone Zone { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [Required]
        public string FullAddress { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("User")]
        public string UserId {get;set;}
        public virtual User User { get; set; }
    }
}