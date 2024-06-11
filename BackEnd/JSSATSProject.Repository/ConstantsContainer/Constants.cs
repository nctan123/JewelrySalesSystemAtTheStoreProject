namespace JSSATSProject.Repository.ConstantsContainer;

public static class Constants
{
    public const string InvalidPhoneNumberFormat = "Invalid phone number format";
    public const string CustomerNotFound = "Customer not found";
    public const string Success = "Success";
    public const string PurchaseOrder = "(?i)sell";
    public const string BuybackOrder = "(?i)buyback";
    public const string ExchangeOrder  = "(?i)exchange";
    public const string RecallOrder = "(?i)recall"; //thu lai hang dc bao hanh voi 100% von
    public const string OrderTypeRegex = "^(?i)(sell|buyback|exchange|recall)$";
    
}