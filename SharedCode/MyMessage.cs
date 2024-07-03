using EasyNetQ;

namespace ClassLibrary1;

[Queue(queueName: "test", ExchangeName = "test")]
public class MyMessage
{
    public string Text { get; set; }
}