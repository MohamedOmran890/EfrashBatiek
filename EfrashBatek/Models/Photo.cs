using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Photo
    {
        public int ID { get; set; }
        [Required]
        public string Image { get; set; }
        [ForeignKey("Design")]
        public int DesignID { get; set; }
        public virtual Design Design { get; set; }


    }
}