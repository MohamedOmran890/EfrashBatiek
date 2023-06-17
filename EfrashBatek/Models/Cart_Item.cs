using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{


    [JsonObject(MemberSerialization.OptIn)]
    public class Cart_Item
    {

        [JsonProperty]
        public int Quantity { get; set; }
        [JsonProperty]
        public int CartID { get; set; }

        public virtual Cart Cart { get; set; }
        [ForeignKey("Item")]

        [JsonProperty]
        public int ItemID { get; set; }

        

        public string ItemName { get; set; }

        public virtual Item Item { get; set; }

    }
}