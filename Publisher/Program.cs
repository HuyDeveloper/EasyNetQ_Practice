using EasyNetQ;
using System;
using System.Threading.Tasks;
using ClassLibrary1;

public class RabbitMqPublisher
{
    private readonly IBus _bus;

    public RabbitMqPublisher(string connectionString)
    {
        _bus = RabbitHutch.CreateBus(connectionString);
    }

    public async Task PublishMessageAsync<T>(T message) where T : class
    {
        await _bus.PubSub.PublishAsync(message).ContinueWith(t =>
        {
            if (t is { IsCompleted: true, IsFaulted: false })
            {
                Console.WriteLine("Message published.");
            }
            else
            {
                throw new EasyNetQException("Failed to publish message.", t.Exception);
            }
        });
    }

    public void Dispose()
    {
        _bus.Dispose();
    }
}



// Usage example
public class Program
{
    public static async Task Main(string[] args)
    {
        var publisher = new RabbitMqPublisher("host=localhost:5672;username=guest;password=guest;publisherConfirms=true;timeout=10");
        var payment1 = new CardPayment("payment1", "nothing1", "date1", 1200);
        var payment2 = new CardPayment("payment2", "nothing2", "date2", 1300);
        var payment3 = new CardPayment("payment3", "nothing3", "date3", 1400);
        
        var purchaseOrder = new PurchaseOrder("purchase","IBD", 1000, 100);
        await publisher.PublishMessageAsync(payment1);
        await publisher.PublishMessageAsync(payment2);
        await publisher.PublishMessageAsync(payment3);
        await publisher.PublishMessageAsync(purchaseOrder);
        publisher.Dispose();
    }
}