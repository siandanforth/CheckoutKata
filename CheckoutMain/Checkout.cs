using System;
using System.Collections.Generic;

namespace CheckoutMain
{
    public class Checkout
    {
        public decimal total { get; set; }
        public Dictionary<string, Item> basket;
        public Checkout()
        {
            total = 0;
            basket = new Dictionary<string, Item>();
        }
        public void ScanItem(Item item)
        {
            if (!basket.ContainsKey(item.sku))
            {
                basket.Add(item.sku, item);
                Console.WriteLine($"Scanned + Added {item.quantity} {item.sku} to basket");
            }
            else
            {
                basket[item.sku].quantity += item.quantity;
                Console.WriteLine($"Scanned + Added {item.quantity} {item.sku} to basket");
                basket[item.sku].subtotalWithDiscount += item.price * item.quantity;
                Console.WriteLine($"Scanned + Updated {item.sku} quantity by {item.quantity}");
            }
        }
        public void RemoveItem(Item item)
        {
            basket.Remove(item.sku);
            Console.WriteLine($"Removed {item.quantity} {item.sku} from basket");
        }
        public decimal CalculateTotal()
        {
            decimal newTotal = 0;
            foreach (var item in basket.Values)
            {
                newTotal += item.subtotalWithDiscount;
                Console.WriteLine($"Your total is {newTotal}");
            }
            total = newTotal;
            return total;
        }
        public void ApplyOffer(Offer offer)
        {
            if (basket.ContainsKey(offer.sku) && !basket[offer.sku].discountApplied)
            {
                Item item = basket[offer.sku];
                int count = item.quantity;
                decimal subtotal = Decimal.Multiply(offer.threshold, item.price);
                while (count >= offer.threshold)
                {
                    item.subtotalWithDiscount = item.subtotalWithDiscount - subtotal + offer.discountPrice;
                    count -= offer.threshold;
                    Console.WriteLine($"Discount applied to {item.sku}");
                }
            }
        }
    }
}
