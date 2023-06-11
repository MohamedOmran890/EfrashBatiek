using Microsoft.AspNetCore.Http;

namespace EfrashBatek.ViewModel
{
    public class ItemVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
        public IFormFile Image4 { get; set; }
        public IFormFile Image5 { get; set; }
        public string discount { get; set; }
        public int PriceAfterSale { get; set; }
        public int QuantityInStore { get; set; }


    }
}
