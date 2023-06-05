using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.ViewModel
{
    public class ResetPasswordVM
    {
      public int ID { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string ConfiremPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
