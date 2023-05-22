using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Cart_Item
    {

        public int Quantity { get; set; }
        public int CartID { get; set; }
        public virtual  Cart Cart { get; set; }
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }

    }
}