using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EfrashBatek.ViewModel
{
    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
      
    }
}
