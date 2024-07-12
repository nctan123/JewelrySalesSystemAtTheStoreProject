using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.PointModel;

public class RequestUpdatePoint
{
    [Range(0, Int32.MaxValue)]
    public int? AvailablePoint { get; set; }
}