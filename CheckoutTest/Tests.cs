using NUnit.Framework;
using CheckoutMain;

namespace CheckoutTest
{
    public class Tests
    {
        Checkout testCheckout;

        [SetUp]
        public void Setup()
        {
            testCheckout = new Checkout();
        }

        [Test]
        public void Test_Get_Total() => Assert.AreEqual(testCheckout.total, 0);

        [Test]
        public void Test_Add_One_Of_New_Item()
        {
            testCheckout.ScanItem(new Item("C40:Coffee", 00.60m, 1));
            Assert.AreEqual(testCheckout.basket.Count, 1);
        }
        [Test]

        public void Test_Add_Many_Of_New_Item()
        {
            testCheckout.ScanItem(new Item("C40:Coffee", 00.60m, 3));
            Assert.AreEqual(testCheckout.basket["C40:Coffee"].quantity, 3);
        }
        [Test]
        public void Test_Increment_Existing_Item()
        {
            testCheckout.ScanItem(new Item("C40:Coffee", 00.60m, 1));
            Assert.AreEqual(testCheckout.basket.Count, 1);
            Assert.AreEqual(testCheckout.basket["C40:Coffee"].quantity, 1);
            testCheckout.ScanItem(new Item("C40:Coffee", 00.60m, 1));
            Assert.AreEqual(testCheckout.basket.Count, 1);
            Assert.AreEqual(testCheckout.basket["C40:Coffee"].quantity, 2);
        }
        [Test]
        public void Test_Remove_Existing_Item()
        {
            Item testItem = new Item("C40:Coffee", 00.60m, 1);
            testCheckout.ScanItem(testItem);
            testCheckout.RemoveItem(testItem);
            Assert.True(!testCheckout.basket.ContainsKey("C40:Coffee"));
        }
        [Test]
        public void Calculate_Total()
        {
            Item firstCoffee = new Item("C40:Coffee", 00.60m, 1);
            Item secondCoffee = new Item("C40:Coffee", 00.60m, 1);
            testCheckout.ScanItem(firstCoffee);
            testCheckout.ScanItem(secondCoffee);
            Assert.AreEqual(testCheckout.CalculateTotal(), 01.20m);
        }

        [Test]
        public void Test_Apply_Offer()
        {
            Item apple = new Item("A99:Apple", 00.50m, 3);
            Item biscuit = new Item("B15:Biscuit", 00.30m, 2);
            Offer biscuitOffer = new Offer(2, 00.45m, "B15:Biscuit");
            Offer appleOffer = new Offer(3, 01.30m, "A99:Apple");
            testCheckout.ScanItem(apple);
            testCheckout.ScanItem(biscuit);
            testCheckout.ApplyOffer(appleOffer);
            testCheckout.ApplyOffer(biscuitOffer);
            Assert.AreEqual(testCheckout.CalculateTotal(), 01.75m);
        }
        [Test]
        public void Test_No_Offer_Applied()
        {
            Item apple = new Item("A99:Apple:Apple", 00.50m, 1);
            Item biscuit = new Item("B15:Biscuit:Biscuit", 00.30m, 1);
            Offer biscuitOffer = new Offer(2, 00.45m, "B15:Biscuit:Biscuit");
            Offer appleOffer = new Offer(3, 01.30m, "A99:Apple:Apple");
            testCheckout.ScanItem(apple);
            testCheckout.ScanItem(biscuit);
            testCheckout.ApplyOffer(appleOffer);
            testCheckout.ApplyOffer(biscuitOffer);
            Assert.AreEqual(testCheckout.CalculateTotal(), 00.80m);
        }
        [Test]
        public void Test_Apply_Offer_Once_Only()
        {
            Item apple = new Item("A99:Apple", 00.50m, 4);
            Item biscuit = new Item("B15:Biscuit", 00.30m, 3);
            Offer biscuitOffer = new Offer(2, 00.45m, "B15:Biscuit");
            Offer appleOffer = new Offer(3, 01.30m, "A99:Apple");
            testCheckout.ScanItem(apple);
            testCheckout.ScanItem(biscuit);
            testCheckout.ApplyOffer(appleOffer);
            testCheckout.ApplyOffer(biscuitOffer);
            Assert.AreEqual(testCheckout.CalculateTotal(), 02.55m);
        }
    }
}