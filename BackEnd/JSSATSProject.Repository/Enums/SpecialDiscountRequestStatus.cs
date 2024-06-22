namespace JSSATSProject.Repository.Enums;

using System.Runtime.Serialization;

public enum SpecialDiscountRequestStatus
{
    [EnumMember(Value = "awaiting")]
    Awaiting,
    
    [EnumMember(Value = "approved")]
    Approved,
    
    [EnumMember(Value = "rejected")]
    Rejected,
    
    [EnumMember(Value = "used")]
    Used
}
