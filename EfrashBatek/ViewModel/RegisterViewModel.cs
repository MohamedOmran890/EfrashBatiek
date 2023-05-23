using System.ComponentModel.DataAnnotations;
using System;
using EfrashBatek.Models;

namespace EfrashBatek.ViewModel
{
    public class RegisterViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First name can only contain alphabetical characters.")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First name can only contain alphabetical characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "UserName Is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
