using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.Models
{
    public enum ProductName
    {
        IKEA,
        Wayfair,
        Room,
        Pottery_Barn
    }

    public class Product
    {

        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual  Category Category { get; set; }
        public virtual  ProductName ProductName { get; set; }
        public virtual ICollection<Item>Items { get; set; }

    }
}