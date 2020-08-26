using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngineCodility;
using System.Collections.Generic;

namespace PromotionEngineCodilityTest
{
    [TestClass]
    public class PromotionCheckTest
    {
        List<Promotion> promotions;

        [TestInitialize]
        public void InitialSetup()
        {
            //create list of promotions, we need to add information about Product's count
            Dictionary<String, int> promo1 = new Dictionary<String, int>();
            promo1.Add("A", 3);
            Dictionary<String, int> promo2 = new Dictionary<String, int>();
            promo2.Add("B", 2);
            Dictionary<String, int> promo3 = new Dictionary<String, int>();
            promo3.Add("C", 1);
            promo3.Add("D", 1);

            promotions = new List<Promotion>()
            {
                new Promotion(1,promo1,130),
                new Promotion(2,promo2,45),
                new Promotion(3,promo3,30)
            };
        }

        [TestMethod]
        public void Scenario_A()
        {
            //1 * A 50
            //1 * B 30
            //1 * C 20
            //======
            //Total 100

            //create order
            Order order = new Order(1, new List<Product>() {
                new Product("A"),
                new Product("B"),
                new Product("C")
            });

            decimal expected = 100;
            decimal actual = PromotionEngine.GetTotalOrderPrice(promotions, order);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Scenario_B()
        {
            //5 * A 130 + 2*50
            //5 * B 45 + 45 + 30
            //1 * C 20
            //======
            //Total 370

            //create order
            Order order2 = new Order(2, new List<Product>() {
                new Product("A"), new Product("A"), new Product("A"), new Product("A"), new Product("A"),
                new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("B"),
                new Product("C")
            });

            decimal expected = 370;
            decimal actual = PromotionEngine.GetTotalOrderPrice(promotions, order2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Scenario_C()
        {
            //3 * A 130
            //5 * B 45 + 45 + 1 * 30
            //1 * C -
            //1 * D 30
            //======
            //Total 280

            //create order
            Order order3 = new Order(3, new List<Product>() {
                new Product("A"), new Product("A"), new Product("A"),
                new Product("B"), new Product("B"), new Product("B"), new Product("B"), new Product("B"),
                new Product("C"),
                new Product("D")
            });

            decimal expected = 280;
            decimal actual = PromotionEngine.GetTotalOrderPrice(promotions, order3);
            Assert.AreEqual(expected, actual);
        }
    }
}
