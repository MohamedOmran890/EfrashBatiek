﻿using EfrashBatek.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.ViewModel
{
    public class RegisterViewModelStaff
    {
        [Key]
        public int Id { get; set; }
        [Required]
        //RegularExpression("^[A-Za-z]", ErrorMessage = "First name can only contain alphabetical characters.")]
        public string FirstName { get; set; }
        [Required]
       // [RegularExpression("^[A-Za-z]", ErrorMessage = "First name can only contain alphabetical characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "UserName Is Required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
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
        public Gender Gender { get; set; }
        [Required]
        
        public int ShopNumber { get; set; }
    }
}