using System;

namespace CheckoutMain
{
    public class Offer
    {
        public int threshold { get; set; }
        public decimal discountPrice { get; set; }
        public string sku { get; set; }
        public Offer(int newThreshold, decimal newDiscountPrice, string newSku)
        {
            threshold = newThreshold;
            discountPrice = newDiscountPrice;
            sku = newSku;
        }
    }
}