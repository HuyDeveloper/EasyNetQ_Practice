// using EasyNetQ;
// using System;
// using System.Threading.Tasks;
// using ClassLibrary1;
//
// public class RabbitMqPublisher : IDisposable
// {
//     private readonly IBus _bus;
//
//     public RabbitMqPublisher(string connectionString)
//     {
//         _bus = RabbitHutch.CreateBus(connectionString);
//     }
//
//     public async Task<U> PublishMessageAsync<T, U>(T message) where T : class
//     {
//         try
//         {
//             var response = await _bus.Rpc.RequestAsync<T, U>(message).ConfigureAwait(false);
//             Console.WriteLine(response);
//             Console.WriteLine("Message published.");
//             return response;
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine($"Failed to publish message: {ex.Message}");
//             throw new EasyNetQException("Failed to publish message.", ex);
//         }
//     }
//
//     public void Dispose()
//     {
//         _bus.Dispose();
//     }
// }
//
// // Usage example
// public class Program
// {
//     public static async Task Main(string[] args)
//     {
//         var connectionString = "host=localhost:5672;username=guest;password=guest;publisherConfirms=true;timeout=10";
//         using (var publisher = new RabbitMqPublisher(connectionString))
//         {
//             var request = new CardPaymentRequestMessage
//             {
//                 Amount = 1000,
//                 CardHolderName = "John Doe",
//                 CardNumber = "1234567890",
//                 ExpiryDate = "12/24"
//             };
//
//             var response = await publisher.PublishMessageAsync<CardPaymentRequestMessage, CardPaymentResponseMessage>(request);
//             Console.WriteLine($"Received response: {response.AuthCode}");
//         }
//     }
// }

using ClassLibrary1;
using EasyNetQ;

class Program
{
    static void Main(String[] argvs)
    {
        var payment = new CardPaymentRequestMessage
        {
            Amount = 1000,
            CardHolderName = "John Doe",
            CardNumber = "1234567890",
            ExpiryDate = "12/24"
        };

        using var bus = RabbitHutch.CreateBus("host=localhost:5672;username=guest;password=guest");
        Console.WriteLine("Publishing message with request and response.");
        Console.WriteLine();
        var response = bus.Rpc.Request<CardPaymentRequestMessage, CardPaymentResponseMessage>(payment);
        Console.WriteLine($"Received response: {response.AuthCode}");
            
        Console.WriteLine("Response received");
    }
}