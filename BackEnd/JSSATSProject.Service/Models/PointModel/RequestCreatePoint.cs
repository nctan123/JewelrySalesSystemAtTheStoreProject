using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.PointModel;

public class RequestCreatePoint
{
    [Range(0, Int32.MaxValue)]
    public int? AvailablePoint { get; set; }
    
    [Range(0, Int32.MaxValue)]
    public int? TotalPoints { get; set; }
}