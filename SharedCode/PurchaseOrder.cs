using EasyNetQ;

namespace ClassLibrary1;

[Queue(queueName: "test1", ExchangeName = "test1")]
public class PurchaseOrder : IPayment
{
    public string PoNumber { get; set; }
    public string CompanyName { get; set; }
    public int PaymentDayTerms { get; set; }
    
    public decimal Amount { get; set; }
    
    public PurchaseOrder(string poNumber, string companyName, int paymentDayTerms, decimal amount)
    {
        PoNumber = poNumber;
        CompanyName = companyName;
        PaymentDayTerms = paymentDayTerms;
        Amount = amount;
    }
}