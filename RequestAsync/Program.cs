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
        var response = bus.Rpc.RequestAsync<CardPaymentRequestMessage, CardPaymentResponseMessage>(payment);
        response.ContinueWith(res =>
        {
            Console.WriteLine($"Received response: {res.Result.AuthCode}");
        });
            
        Console.WriteLine("Response received");
    }
}