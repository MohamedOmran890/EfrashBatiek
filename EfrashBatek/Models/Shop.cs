using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.Models
{
    public class Shop
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShopAddress {get; set; }
        [Required]
        public string TaxCardImage { get; set; }
        public int ShopNumber { get; set; }
         public string PhoneNumber { get; set; }
        public virtual  ICollection<Order_Item> Order_Item { get; set; }
        public virtual  ICollection<Item> Item { get; set; }
        public virtual  ICollection<Staff> Staff { get; set; }





    }
}