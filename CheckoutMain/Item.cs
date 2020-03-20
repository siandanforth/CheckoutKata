using System;

namespace CheckoutMain
{
    public class Item
    {
        public string sku { get; }
        public decimal price { get; }
        public int quantity { get; set; }
        public decimal subtotalWithDiscount { get; set; }
        public bool discountApplied { get; set; }
        public Item(string newSku, decimal newPrice, int newQuantity = 1)
        {
            quantity = newQuantity;
            sku = newSku;
            price = newPrice;
            subtotalWithDiscount = price * quantity;
            discountApplied = false;
        }
    }
}