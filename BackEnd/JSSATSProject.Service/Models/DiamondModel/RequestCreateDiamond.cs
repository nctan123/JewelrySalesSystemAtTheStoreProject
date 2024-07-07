using JSSATSProject.Repository.CustomValidators;

namespace JSSATSProject.Service.Models.DiamondModel;

public class RequestCreateDiamond
{
    public int OriginId { get; set; }

    public int ShapeId { get; set; }

    public int FluorescenceId { get; set; }

    public int ColorId { get; set; }

    public int SymmetryId { get; set; }

    public int PolishId { get; set; }

    public int CutId { get; set; }
    public int ClarityId { get; set; }

    public int CaratId { get; set; }
    
    [StatusValidator("active", "inactive")]
    public string? Status { get; set; }

    public string DiamondGradingCode { get; set; }
}