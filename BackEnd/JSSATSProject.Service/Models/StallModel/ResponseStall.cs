namespace JSSATSProject.Service.Models.StallModel;

public class ResponseStall
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TypeId { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }
}