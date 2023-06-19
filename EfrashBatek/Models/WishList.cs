using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class WishList
    {
        public int Id { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public virtual ICollection<WishListItem> WishListItems { get; set; }

    }
}