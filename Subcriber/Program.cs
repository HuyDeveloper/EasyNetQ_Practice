using EasyNetQ;
using System;
using System.Threading.Tasks;
using ClassLibrary1;

public class RabbitMqSubscriber
{
    private readonly IBus _bus;

    public RabbitMqSubscriber(string connectionString)
    {
        _bus = RabbitHutch.CreateBus(connectionString);
    }

    public async Task SubscribeAsync<T>(string subscriptionId, Action<T> onMessage) where T : class
    {
        await _bus.PubSub.SubscribeAsync(subscriptionId, onMessage, c => c.WithQueueName("test"));
        Console.WriteLine("Subscribed to messages.");
    }

    public void Dispose()
    {
        _bus.Dispose();
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        var subscriber = new RabbitMqSubscriber("host=localhost:5672;username=guest;password=guest");

        subscriber?.SubscribeAsync<IPayment>("test",
            message => HandlerMessage(message));
        Console.WriteLine("Listening for messages. Press [Enter] to exit.");
        Console.ReadLine();
        subscriber?.Dispose();
    }

    private static void HandlerMessage(IPayment message)
    {
        var cardPayment = message as CardPayment;
        var purchaseOrder = message as PurchaseOrder;
        if (cardPayment != null)
        {
            Console.WriteLine(
                $"Received payment: {cardPayment?.CardNumber}; {cardPayment?.CardHolderName}; {cardPayment?.ExpiryDate}; {cardPayment.Amount}");
        }

        if (purchaseOrder != null)
        {
            Console.WriteLine(
                $"Purchase order: {purchaseOrder?.PoNumber}; {purchaseOrder?.Amount}; {purchaseOrder?.CompanyName}; {purchaseOrder?.PaymentDayTerms}");
        }
    }
}