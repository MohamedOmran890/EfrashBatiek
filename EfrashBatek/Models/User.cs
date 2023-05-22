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
        Customer,
        Staff,
        admin,
        Designer,
    }
    public class User
    {

        [Key]
        public int ID { get; set; }
        public int age { get; set; }
        [Required]
        public string phone { get; set; }

        [Column(TypeName = "Date")]
        public DateTime BirthDate{ get; set; }
        public virtual ICollection<Address> Address { get; set; }   
        public virtual ICollection<Video> Videos { get; set; }

        public  Gender Gender { get; set; }

        public UserType UserType { get; set; }
        [ForeignKey("identityUser")]
        public string IdentityId { get; set; }
        public IdentityUser identityUser { get; set; }
        /*testtest*/
    }
    
}
