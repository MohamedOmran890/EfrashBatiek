using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.ViewModel
{
    public class DashBoardShopVM
    {
        
            public int Totalshop { get; set; }
            public int TotalCustomers { get; set; }
            public int TotalOrders { get; set; }
            public List<Order_Item> orders { get; set; }


}
}
