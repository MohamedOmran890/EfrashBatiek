using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Staff
    {

        [Key("ID")]
        public int ID { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Shop")]
        public string  ShopID { get; set; }
        public virtual Shop Shop { get; set; }

    }
}