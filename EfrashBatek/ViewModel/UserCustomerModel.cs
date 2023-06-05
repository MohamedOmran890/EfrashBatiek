using EfrashBatek.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ExceptionServices;

namespace EfrashBatek.ViewModel
{
    public class UserCustomerModel 
    {
        public string FirstName;
        public string LastName;
        public string Email;    
        public string PhoneNumber;
        public string  id;
        public Zone zone;
    }
}
