using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{

    [Serializable]
    public class Cart
    {
        [JsonProperty]
        public int ID { get; set; }
        [Required]
        [JsonProperty]
        public DateTime Date { get; set; }
        [ForeignKey("Customer")]
        [JsonProperty]
        public int CustomerID { get; set; }
        [JsonProperty]
        public virtual Customer Customer { get; set; }
        [JsonProperty]
        public virtual ICollection<Cart_Item> items { get; set; }

    }
}