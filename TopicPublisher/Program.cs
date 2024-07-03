using ClassLibrary1;
using EasyNetQ;

class Program
{
    static void Main(String[] args)
    {
        var cardPayment1 = new CardPayment("John Doe", "1000", "1234567890", 1000);
        var cardPayment2 = new CardPayment("Jane Doe", "2000", "0987654321", 2000);
        var cardPayment3 = new CardPayment("John Smith", "3000", "1234567890", 3000);
        
        var purchaseOrder1 = new PurchaseOrder("purchase", "IBD", 1000, 100);
        var purchaseOrder2 = new PurchaseOrder("purchase", "IBD", 2000, 200);
        var purchaseOrder3 = new PurchaseOrder("purchase", "IBD", 3000, 300);
        
        var bus = RabbitHutch.CreateBus("host=localhost:5672;username=guest;password=guest");
        bus.PubSub.Publish<IPayment>(cardPayment1, "payment.cardPayment");
        bus.PubSub.Publish<IPayment>(purchaseOrder1, "payment.purchaseOrder");
        bus.PubSub.Publish<IPayment>(cardPayment2, "payment.cardPayment");
        bus.PubSub.Publish<IPayment>(purchaseOrder2, "payment.purchaseOrder");
        Console.WriteLine("Published messages.");
    }
}