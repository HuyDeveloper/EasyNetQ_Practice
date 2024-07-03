// using EasyNetQ;
// using System;
// using System.Threading.Tasks;
// using ClassLibrary1;
//
// public class RabbitMqSubscriber : IDisposable
// {
//     private readonly IBus _bus;
//
//     public RabbitMqSubscriber(string connectionString)
//     {
//         _bus = RabbitHutch.CreateBus(connectionString);
//     }
//
//     public void RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder) 
//         where TRequest : class 
//         where TResponse : class
//     {
//         _bus.Rpc.RespondAsync(responder);
//         Console.WriteLine("Subscribed to messages.");
//     }
//
//     public void Dispose()
//     {
//         _bus.Dispose();
//     }
// }
//
// public class Program
// {
//     public static void Main(string[] args)
//     {
//         var connectionString = "host=localhost:5672;username=guest;password=guest";
//         using (var subscriber = new RabbitMqSubscriber(connectionString))
//         {
//             subscriber.RespondAsync<CardPaymentRequestMessage, CardPaymentResponseMessage>(Responder);
//
//             Console.WriteLine("Listening for messages. Press [Enter] to exit.");
//             Console.ReadLine();
//         }
//     }
//
//     private static Task<CardPaymentResponseMessage> Responder(CardPaymentRequestMessage request)
//     {
//         return Task.FromResult(new CardPaymentResponseMessage { AuthCode = "2134" });
//     }
// }

using ClassLibrary1;
using EasyNetQ;

class Program
{
    static void Main(string[] args)
    {
        using (var bus = RabbitHutch.CreateBus("host=localhost:5672;username=guest;password=guest"))
        {
            bus.Rpc.Respond<CardPaymentRequestMessage, CardPaymentResponseMessage>(Responsder);
            Console.WriteLine("Listening for messages. Press [Enter] to exit.");
            Console.ReadLine();
        }
    }

    private static CardPaymentResponseMessage Responsder(CardPaymentRequestMessage request)
    {
        return new CardPaymentResponseMessage { AuthCode = "1234" };
    }
}