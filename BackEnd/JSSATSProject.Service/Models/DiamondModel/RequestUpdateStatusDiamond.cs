using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Service.Models.DiamondModel;

public class RequestUpdateStatusDiamond
{
    [StatusValidator("active", "inactive")]
    public string? Status { get; set; }
}