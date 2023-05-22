using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public enum Reason:short
    {
        Damaged=1,
        PoorQuality,

    }
    public class Warrantly_Request
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("Order_Item")]
        public int Order_ItemID { get; set; }
        public virtual Order_Item Order_Item { get; set; }
        public Reason Reason { get; set; }


    }
}