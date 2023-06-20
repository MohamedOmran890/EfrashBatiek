using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.Models
{
    public enum ProductName
    {
        Wood = 1,
        Electrics,
        Acrylic,
        LivingRoom,
        YouthAndKidsBedRooms,
        DiningRoom,
        LargeAppliances,
        KitchenAppliances,
        HomeAppliances,
        Cookware,
        Drinkware,
        Dinnerware,
        Alometal,

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