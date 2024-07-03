using ClassLibrary1;
using EasyNetQ;
using System;

class Program
{
    public static void Main(string[] args)
    {
        var bus = RabbitHutch.CreateBus("host=localhost;username=guest;password=guest");

        bus.SendReceive.Receive("cardPayment",
            x => x.Add<CardPaymentRequestMessage>(msg =>
            {
                Console.WriteLine($"Received payment request: {msg.CardHolderName}, {msg.Amount}");
            }).Add<PurchaseOrder>(x =>
            {
                Console.WriteLine($"Received purchase order: {x.CompanyName}, {x.CompanyName}");
            }));
        Console.WriteLine("Listening for messages. Press [Enter] to exit.");
        Console.ReadLine();
        bus.Dispose();
    }
}