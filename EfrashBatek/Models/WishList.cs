using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class WishList
    {
        public int ID { get; set; }
       

        public virtual List<Item> Items { get; set; }   
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

    }
}