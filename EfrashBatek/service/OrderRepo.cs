using EfrashBatek.Models;
using System.Linq;
using System.Runtime.InteropServices;

namespace EfrashBatek.service
{ 
    public class OrderRepo 
    {
        
        Context context;
        public OrderRepo (Context context) {

            this.context = context;

        }
      
        public  void Update (Order Order , int CustomerID ) { 
        
        }
        public Order View  (int CustomerID ) {
            Order order = new Order();
            return order ;
        }
        
      
      
    }
}
