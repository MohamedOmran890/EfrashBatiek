using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Cart
    {
        public int ID { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Cart_Item>Cart_Item { get; set; }

    }
}