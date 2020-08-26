using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineCodility
{
    public class Order
    {
        public int OrderID { get; set; }
        public List<Product> Products { get; set; }

        public Order(int _orderID, List<Product> _products)
        {
            this.OrderID = _orderID;
            this.Products = _products;
        }
    }
}
