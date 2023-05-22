using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
    public class Staff
    {

        [ForeignKey("User")]
        [Key("ID")]
        public int ID { get; set; }
        public User User { get; set; }

        [ForeignKey("Shop")]
        public int ShopID { get; set; }
        public virtual Shop Shop { get; set; }

    }
}