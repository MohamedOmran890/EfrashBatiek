using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.ViewModel
{
    public class DashboardViewModel
    {
        public int Totalshop { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }
        public List <Order> orders { get; set; }    

    }
}
