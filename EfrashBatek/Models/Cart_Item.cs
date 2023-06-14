using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Cart_Item
    {

        public int Quantity { get; set; }

        public int CartID { get; set; }
        public virtual Cart Cart { get; set; }
        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public itemData itemData { get;set;}
        public string ItemName { get; set; }
        public virtual Item Item { get; set; }

    }
}