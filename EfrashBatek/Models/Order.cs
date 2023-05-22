using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public enum PaymentMethod{
        CashOnDelivery=1,
        CreditCard,


    }
    public class Order
    {
        public int ID { get; set; }
        public int OrderCode { get; set; }
        public double TotalCost { get; set; }
        [Column(TypeName ="Date")]
        public DateTime OrderDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        [ForeignKey("Address")]
        public int AddressID { get; set; }
        public virtual Address Address  { get; set; }
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual  Customer Customer { get; set; }

        public virtual  ICollection<Order_Item> Order_Item { get; set; }




    }
}