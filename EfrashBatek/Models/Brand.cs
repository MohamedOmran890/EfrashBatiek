using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.Models
{
    public enum Category
    {
        furniture,
        Home_Appliances,
        Kitchen_utensils,
        Kitchen , 
        All_Products =10
    }
    public class Brand
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string logo { get; set; }
        public virtual Category Category { get; set; }

    }
}