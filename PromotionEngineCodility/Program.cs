using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineCodility
{
    class Program
    {
        static void Main(string[] args)
        {
            //create list of promotions, we need to add information about Product's count
            Dictionary<String, int> promo1 = new Dictionary<String, int>();
            promo1.Add("A", 3);
            Dictionary<String, int> promo2 = new Dictionary<String, int>();
            promo2.Add("B", 2);
            Dictionary<String, int> promo3 = new Dictionary<String, int>();
            promo3.Add("C", 1);
            promo3.Add("D", 1);

            List<Promotion> promotions = new List<Promotion>()
            {
                new Promotion(1,promo1,130),
                new Promotion(2,promo2,45),
                new Promotion(3,promo3,30)
            };

            //create orders
            List<Order> orders = new List<Order>();
            Order order1 = new Order(1, new List<Product>() {
                new Product("A"),
                new Product("B"),
                new Product("C")
            });

            Order order2 = new Order(2, new List<Product>() {
                new Product("A"), new Product("A"), new Product("A"), new Product("A"), new Product("A"),
                new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("B"),
                new Product("C")
            });

            Order order3 = new Order(3, new List<Product>() {
                new Product("A"), new Product("A"), new Product("A"),
                new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("B"),
                new Product("C"),
                new Product("D")
            });

            Order order4 = new Order(4, new List<Product>() {
                new Product("A"),
                new Product("B"),new Product("B"),
                new Product("C"),
                new Product("D")
            });

            orders.AddRange(new Order[] { order1, order2, order3, order4 });

            foreach (Order ord in orders)
            {
               decimal totalPrice= PromotionEngine.GetTotalOrderPrice(promotions, ord);
               Console.WriteLine($"OrderID: {ord.OrderID} => Total price: {totalPrice.ToString("0.00")}");
            }

            Console.ReadKey();
        }
    }
}
