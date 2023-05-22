using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Design
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("Designer")]
        public int DesignerID { get; set; }
        public virtual Designer Designer { get; set; }
        public virtual ICollection<Photo>Photos { get; set; }
    }
}