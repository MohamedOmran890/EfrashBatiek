using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class WishListItem
    {
        public int Id { get; set; }

        [ForeignKey("WishList")]
        public int WishListId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public Item Item { get; set; }
        public WishList WishList { get; set; }

    }
}
