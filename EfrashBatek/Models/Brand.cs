using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.Models
{
    public enum Category
    {
        Clothes,
        Electronic,
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