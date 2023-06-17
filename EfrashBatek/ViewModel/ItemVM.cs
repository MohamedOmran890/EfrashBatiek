using EfrashBatek.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace EfrashBatek.ViewModel
{
    public class ItemVM
    {
        public int ID { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Unit Price")]
        public double Price { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
        public IFormFile Image4 { get; set; }
        public IFormFile Image5 { get; set; }
        public string discount { get; set; }
        public int PriceAfterSale { get; set; }
        public int Brand_ID { get; set; }
        [Required]
        public int QuantityInStore { get; set; }
        public int ShopID { get; set; }
        public int ProductID { get; set; }
        public DateTime DateTime { get; set; }

    }
}
