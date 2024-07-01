using System.Runtime.Serialization;

namespace JSSATSProject.Repository.Enums;

public enum SpecialDiscountRequestStatus
{
    [EnumMember(Value = "awaiting")] Awaiting,

    [EnumMember(Value = "approved")] Approved,

    [EnumMember(Value = "rejected")] Rejected,

    [EnumMember(Value = "used")] Used
}