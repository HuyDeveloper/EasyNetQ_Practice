using EasyNetQ;

namespace ClassLibrary1;

[Queue(queueName: "test1", ExchangeName = "test1")]
public class CardPayment : IPayment
{
    public string CardNumber { get; set; }
    public string CardHolderName { get; set; }
    public string ExpiryDate { get; set; }
    public decimal Amount { get; set; }

    public CardPayment(string cardNumber, string cardHolderName, string expiryDate, decimal amount)
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpiryDate = expiryDate;
        Amount = amount;
    }
}