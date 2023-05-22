using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Item
    {
        public int ID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public double Price { get; set; }
        [Required]
        public string Image { get; set; }
        [ForeignKey("Brand")]
        public int Brand_ID { get; set; }
        public virtual  Brand Brand { get; set; }
        public int QuantityInStore { get; set; }
        [ForeignKey("Shop")]
        public int ShopID { get; set; }
        public virtual  Shop Shop  { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public virtual ICollection<Cart_Item> Cart_Items { get; set; }
        public virtual ICollection<Order_Item> Order_Items { get; set; }
        //public virtual ICollection<Video> Videos { get; set; }


    }
}