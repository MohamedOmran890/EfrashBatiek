using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Feedback
    {
        public int ID { get; set; }
        [Required]
        public string ComplaintMessage { get; set; }
        [ForeignKey("Customer")]
        public int OrderItemID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual  Order_Item Order_Item { get; set; }



    }
}