using EfrashBatek.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ExceptionServices;

namespace EfrashBatek.ViewModel
{
    public class UserCustomerModel 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string  Id { get; set; } 
        public string PhoneNumber { get; set; }
        public Zone zone { get; set; }
    }
}
