using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Admin
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("User")]
       public string UserId { get; set; }
        public User User { get; set; }
    }
}
