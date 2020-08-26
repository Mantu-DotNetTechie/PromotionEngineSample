using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngineCodility
{
    public class Promotion
    {
        public int PromotionID { get; set; }
        public Dictionary<string,int> ProductInfo { get; set; }
        public decimal PromotionPrice { get; set; }

        public Promotion(int _promoID, Dictionary<string,int> _prodInfo, decimal _promoPrice)
        {
            this.PromotionID = _promoID;
            this.ProductInfo = _prodInfo;
            this.PromotionPrice = _promoPrice;
        }
    }
}
