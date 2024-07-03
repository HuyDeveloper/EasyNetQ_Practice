using ClassLibrary1;
using EasyNetQ;

class Program
{
    public static void Main(String[] args)
    {
        var bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest");
        bus.PubSub.Subscribe<IPayment>("purchaseOrder", Handler, x => x.WithTopic("payment.purchaseOrder"));
        Console.WriteLine("Listening for messages. Press [Enter] to exit.");
        Console.ReadLine();
    }

    public static void Handler(IPayment payment)
    {
        if (payment is PurchaseOrder purchaseOrder)
        {
            System.Console.WriteLine($"Received purchase order: {purchaseOrder.CompanyName}, {purchaseOrder.Amount}");
        }
        else
        {
            System.Console.WriteLine("Unknown payment type");
        }
    }
}