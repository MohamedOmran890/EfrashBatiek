using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.ViewModel
{
    public class ForgetPasswordVM
    {
        public int Id { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }

    }
}