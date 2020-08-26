using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineCodility
{
    public static class PromotionEngine
    {
        /// <summary>
        /// Get the Total Products price
        /// </summary>
        /// <param name="order">Order Details</param>
        /// <param name="promotion"> Promotion details</param>
        /// <returns></returns>
        private static decimal GetTotalProductPrice(Order order, Promotion promotion)
        {
            try
            {
                decimal totalPrice = 0m;
                string productID = promotion.ProductInfo.Keys.FirstOrDefault();

                // Get count of promoted products in Order.
                var countPromotedProductsInOrder = order.Products
                    .GroupBy(x => x.Id)
                    .Where(group => promotion.ProductInfo.Any(y => group.Key == y.Key && group.Count() >= y.Value))
                    .Select(group => group.Count())
                    .Sum();

                int totalPromotedProductsInOrder = countPromotedProductsInOrder;

                // Get count of promoted products from promotion
                int countPromotedProdFromPromo = promotion.ProductInfo.Sum(x => x.Value);

                //Calculting total promotion price
                while (countPromotedProductsInOrder>=countPromotedProdFromPromo)
                {
                    totalPrice += promotion.PromotionPrice;
                    countPromotedProductsInOrder -= countPromotedProdFromPromo;
                }

                int countProductsWithSamePromo= order.Products
                    .GroupBy(x => x.Id)
                    .Where(group => promotion.ProductInfo.Any(y => group.Key == y.Key))
                    .Select(group => group.Count())
                    .Sum();

                if (countPromotedProductsInOrder != 0 && countPromotedProductsInOrder < countPromotedProdFromPromo)
                {
                    decimal productPrice = order.Products.Where(x => x.Id == productID).Select(x => x.Price).FirstOrDefault();
                    totalPrice += countPromotedProductsInOrder * productPrice;
                }
                else if(countProductsWithSamePromo!= totalPromotedProductsInOrder && countProductsWithSamePromo < countPromotedProdFromPromo)
                {
                    totalPrice = order.Products.Where(x => x.Id == productID).Select(x => x.Price).Sum();
                }

                return totalPrice;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static decimal GetTotalOrderPrice(List<Promotion> promotions, Order ord)
        {
            decimal totalPricce = promotions
                                .Select(promo => PromotionEngine.GetTotalProductPrice(ord, promo))
                                .ToList().Sum();
            return totalPricce;
        }
    }
}
