namespace JSSATSProject.Repository.ConstantsContainer;

public class OrderConstants
{
    public const string CanceledStatus = "cancelled";
    public const string CompletedStatus = "completed";
    public const string ProcessingStatus = "processing";
    public const string DraftStatus = "draft";
    public const string WaitingForDiscountResponseStatus = "waiting for special discount response";
    public const string WaitingForCustomer = "waiting for customer confirmation for discount";
    public const string WaitingForPayment = "waiting for customer payment";

    public const string SellOrderCodePrefix = "SELL_";
    public const string BuyOrderCodePrefix = "BUY_";
}