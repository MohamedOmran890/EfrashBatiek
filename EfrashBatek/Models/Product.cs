using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.Models
{
    public enum ProductName
    {
        Living_Room=1,
        Youth_Kids_Bed_rooms,
        dining_room,
        large_Appliances,
        kitchen_Appliances,
        Home_Appliances,
        Cookware,
        Drinkware,
        Dinnerware,
        alometal,
        wood,
        acrylic

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