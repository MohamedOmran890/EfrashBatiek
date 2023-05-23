using EfrashBatek.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public class CustomerRepo
    {

        Context context;
        public CustomerRepo(Context context)
        {

            this.context = context;

        }
        public void Create(Order Order, int CustomerID)
        {
            .


             

        }
        public void CancelOrder (int OrderID) { 
        
        
        }
        // all orders cards 
        public List<Order> ViewOrders () {

            var Orders = new List<Order> ();  

            return Orders  ;   
        
        
        }
    

    }
}
