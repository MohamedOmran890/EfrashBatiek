using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Admin
    {
        [ForeignKey("User")]
        [Key]
        public int ID { get; set; }
        public User User { get; set; }
    }
}
