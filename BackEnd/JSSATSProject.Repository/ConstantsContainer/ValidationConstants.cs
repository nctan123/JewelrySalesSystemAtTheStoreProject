namespace JSSATSProject.Repository.ConstantsContainer;

public static class ValidationConstants
{
    public const string InvalidPhoneNumberFormat = "Invalid phone number format";
    public const string CustomerNotFound = "Customer not found";
    public const string Success = "Success";
    public const string PurchaseOrder = "(?i)sell";
    public const string BuyOrder = "(?i)buy";
    public const string OrderTypeRegex = "^(?i)(sell|buy)$";
}