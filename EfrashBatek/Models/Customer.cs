using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public string Image { get; set; }
        [ForeignKey("WishList")]
        public int WishListId { get; set; } 
        public virtual WishList WishList { get; set; }
      
       
        public Cart Cart { get; set; }  
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Custom> Customs { get; set; }

    }
}