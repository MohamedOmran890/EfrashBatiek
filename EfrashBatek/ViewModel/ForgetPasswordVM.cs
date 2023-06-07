using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.ViewModel
{
    public class ForgetPasswordVM
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

    }
}