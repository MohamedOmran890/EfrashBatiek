using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfrashBatek.Models
{
     public enum Gender:short
    {
        male=1,
        famale
    }
    public enum UserType : short
    {
        Customer=1,
        Admin,
        Staff,
        shop,
        Designer,
    }
    public class User:IdentityUser
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public int age { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime BirthDate{ get; set; }
        public virtual Address  Address { get; set; }   
        public virtual ICollection<Video> Videos { get; set; }
        [Required]
        public  Gender Gender { get; set; }
        [Required]
        public UserType UserType { get; set; }
        public Zone zone { get; set; }  
        /*testtest*/
    }
    
}
