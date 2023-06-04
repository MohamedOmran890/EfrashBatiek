using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.ViewModel
{
    public class RoleViewModel
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
    }
}
