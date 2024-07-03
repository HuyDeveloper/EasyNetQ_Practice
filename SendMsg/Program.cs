using ClassLibrary1;
using EasyNetQ;

class Program
{
    public static void Main(String[] args)
    {
        var payment1 = new CardPaymentRequestMessage
        {
            Amount = 1000,
            CardHolderName = "John Doe",
            CardNumber = "1234567890",
            ExpiryDate = "12/24"
        };
        var payment2 = new CardPaymentRequestMessage
        {
            Amount = 2000,
            CardHolderName = "Jane Doe",
            CardNumber = "0987654321",
            ExpiryDate = "12/25"
        };

        var payment3 = new CardPaymentRequestMessage
        {
            Amount = 3000,
            CardHolderName = "John Smith",
            CardNumber = "1234567890",
            ExpiryDate = "12/26"
        };

        var purcharseOrder1 = new PurchaseOrder("purchase", "IBD", 1000, 100);
        
        var purcharseOrder2 = new PurchaseOrder("purchase", "IBD", 2000, 200);
        var purcharseOder3 = new PurchaseOrder("purchase", "IBD", 3000, 300);
        var bus = RabbitHutch.CreateBus("host=localhost:15672;username=guest;password=guest");
        
        Console.WriteLine("Publishing message with request and response.");
        Console.WriteLine();
        
        bus.SendReceive.Send("cardPayment", payment1);
        bus.SendReceive.Send("cardPayment", payment2);
        bus.SendReceive.Send("cardPayment", payment3);
        
        bus.SendReceive.Send("cardPayment", purcharseOrder1);
        bus.SendReceive.Send("cardPayment", purcharseOrder2);
        bus.SendReceive.Send("cardPayment", purcharseOder3);
        
    }
}