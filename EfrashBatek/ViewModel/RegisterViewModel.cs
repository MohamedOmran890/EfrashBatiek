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
        [RegularExpression("^[A-Za-z]*$", ErrorMessage = "First name can only contain alphabetical characters.")]
        public string FirstName { get; set; }
        [Required]
      [RegularExpression("^[A-Za-z]*$", ErrorMessage = "Last name can only contain alphabetical characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "UserName Is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [Range(1, 120)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Age Is Number Only.")]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
