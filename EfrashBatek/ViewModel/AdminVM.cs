using System.ComponentModel.DataAnnotations;

namespace EfrashBatek.ViewModel
{
    public class AdminVM
    {
        public int ID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
