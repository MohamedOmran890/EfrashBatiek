using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public enum OrderState
    {
        Cancelled=1,
        Delivering,
        Delivered,
        Returned,
    }
    public class Order_Item
    {
        public int ID { get; set; }
        [Column(TypeName ="Date")]
        public DateTime HistoryRecieve { get; set; }
        public int Quantity { get; set; }
        public OrderState OrderState { get; set; }

        public virtual  ICollection<Feedback>Feedbacks { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
        [ForeignKey("Shop")]
        public int ShopID { get; set; }
        public virtual  Shop Shop { get; set; }
        [ForeignKey("Warrantly_Request")]
        public int Warrantly_RequestID { get; set; }
        public virtual Warrantly_Request Warrantly_Request { get; set; }
        [ForeignKey("item")]
        public int ItemID { get; set; }
        public virtual Item item { get; set; }
    }
}