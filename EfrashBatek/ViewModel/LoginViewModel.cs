using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EfrashBatek.ViewModel
{
    public class LoginViewModel
    {
            [Required]
            public string UserName { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            //[Display(Name = "Remember me?")]
            //public bool RememberMe { get; set; }
           public bool isPersistent { get; set; }


    }
}
