using EfrashBatek.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
namespace EfrashBatek.ViewModel
{
    public enum Zones : short
    {
        Qena = 1,
        Cairo,
        Assiut,
        Sohag,
        Minya,
        Aswan,
        Luxor
    }
    public class CustomVM
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Zones Zones { get; set; }
         public int CustomerID { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
