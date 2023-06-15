using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Designer
    {
        [Key]
        public string ID { get; set; }
        [ForeignKey("User")]
       public string UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string NationalCardImage { get; set; }
        public virtual ICollection<Design>Design { get; set; }

    }
}
