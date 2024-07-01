namespace JSSATSProject.Service.Models.StallModel;

public class RequestCreateStall
{
    public string Name { get; set; } = null!;

    public int TypeId { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }
}