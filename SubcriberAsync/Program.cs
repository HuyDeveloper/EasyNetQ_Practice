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
    public static async Task Main(string[] args)
    {
        var subscriber = new RabbitMqSubscriber("host=localhost:5672;username=guest;password=guest");

        await subscriber.SubscribeAsync<CardPayment>("test",
            message => Task.Factory.StartNew(() =>
            {
                Console.WriteLine(
                    $"Received message: {message.CardNumber}; {message.CardHolderName}; {message.ExpiryDate}; {message.Amount}");
            }).ContinueWith(t =>
            {
                if (t.IsCompleted && !t.IsFaulted)
                {
                    Console.WriteLine("Finished processing all messages.");
                }
                else
                {
                    throw new EasyNetQException("Failed to process message.", t.Exception);
                }
            }));
        Console.WriteLine("Listening for messages. Press [Enter] to exit.");
        Console.ReadLine();
        subscriber.Dispose();
    }
}