using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class WishList
    {
        public int ID { get; set; }
        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

    }
}