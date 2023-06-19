using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class WishList
    {

            public int ID { get; set; }

            [ForeignKey("Item")]
            public int ItemId { get; set; }
            public Item Item { get; set; }

            [ForeignKey("Customer")]
            public int CustomerId { get; set; }
            public Customer Customer { get; set; }
        

    }
}