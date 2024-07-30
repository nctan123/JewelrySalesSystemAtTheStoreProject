using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace JSSATSProject.Service.Models.StallModel;

public class RequestCreateStall
{
    public string Name { get; set; } = null!;

    [Range(0, (Double.MaxValue))]
    public int TypeId { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }
}