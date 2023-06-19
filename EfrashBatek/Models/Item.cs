using System;
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
        [Display(Name="Product Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        public double Price { get; set; }
        [Required]
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public string discount { get; set; }
        public int PriceAfterSale { get; set; }
        [ForeignKey("Brand")]
        public int Brand_ID { get; set; }
        public virtual Brand Brand { get; set; }
        [Required]
        public int QuantityInStore { get; set; }
        [ForeignKey("Shop")]
        public int ShopID { get; set; }
        public virtual Shop Shop { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public DateTime DateTime { get; set; }  
        public virtual ICollection<Cart_Item> Cart_Items { get; set; }
        public virtual ICollection<Order_Item> Order_Items { get; set; }
        public virtual ICollection<WishListItem> WishListItems { get; set; }
        //public virtual ICollection<Video> Videos { get; set; }


    }
}