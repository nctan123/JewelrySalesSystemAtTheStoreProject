namespace JSSATSProject.Service.Models.DiamondModel;

public class ResponseDiamond
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string OriginName { get; set; }
    public required string ShapeName { get; set; }
    public required string FluorescenceName { get; set; }
    public required string ColorName { get; set; }
    public required string SymmetryName { get; set; }
    public required string PolishName { get; set; }
    public required string CutName { get; set; }
    public required string ClarityName { get; set; }
    public required decimal CaratWeight { get; set; }
    public required string DiamondGradingCode { get; set; }
}