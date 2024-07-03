using ClassLibrary1;
using EasyNetQ;

class Program
{
    public static void Main(String[] args)
    {
        var bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest");
        bus.PubSub.Subscribe<IPayment>("accounts", Handler, x => x.WithTopic("payment.*"));
        Console.WriteLine("Listening for messages. Press [Enter] to exit.");
        Console.ReadLine();
    }

    public static void Handler(IPayment payment)
    {
        if (payment is CardPayment cardPayment)
        {
            Console.WriteLine($"Received card payment: {cardPayment.CardHolderName}, {cardPayment.Amount}");
        }
        else if (payment is PurchaseOrder purchaseOrder)
        {
            Console.WriteLine($"Received purchase: {purchaseOrder.CompanyName}, {purchaseOrder.Amount}, {purchaseOrder.PoNumber}");
        }
        else
        {
            System.Console.WriteLine("Unknown payment type");
        }
    }
}