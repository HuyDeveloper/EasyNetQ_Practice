using System.Collections.Concurrent;
using ClassLibrary1;
using EasyNetQ;

class  Program
{
    public class Worker
    {
        public CardPaymentResponseMessage Execute(CardPaymentRequestMessage request)
        {
            CardPaymentResponseMessage responseMessage = new CardPaymentResponseMessage();
            responseMessage.AuthCode = "1234";
            Console.WriteLine("Worker executed");

            return responseMessage;
        }
    }

    static void Main(string[] argvs)
    {
        var workers = new BlockingCollection<Worker>();
        for (int i = 0; i < 10; i++)
        {
            workers.Add(new Worker());
        }
        var bus = RabbitHutch.CreateBus("host=localhost:5672;username=guest;password=guest");
        bus.Rpc.RespondAsync<CardPaymentRequestMessage, CardPaymentResponseMessage>(request => 
        Task.Factory.StartNew(() =>
        {
            var worker = workers.Take();
            try
            {
                return worker.Execute(request);
            }
            finally
            {
                workers.Add(worker);
            }
        }));
        Console.WriteLine("Listening for messages. Press [Enter] to exit.");
        Console.ReadLine();
    }
}