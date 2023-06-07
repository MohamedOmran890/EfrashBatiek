using EfrashBatek.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EfrashBatek.ViewModel
{
    public class CustomVM
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Zone Zone { get; set; }
  
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
