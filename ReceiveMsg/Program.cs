using ClassLibrary1;
using EasyNetQ;
using System;

class Program
{
    public static void Main(string[] args)
    {
        var bus = RabbitHutch.CreateBus("host=localhost:15672;username=guest;password=guest");

        bus.SendReceive.Receive<CardPaymentRequestMessage>("cardPayment", message =>
        {
            Console.WriteLine($"Received payment request: {message.CardHolderName}, {message.Amount}");
        });
        Console.WriteLine("Listening for messages. Press [Enter] to exit.");
        Console.ReadLine();
        bus.Dispose();
    }
}