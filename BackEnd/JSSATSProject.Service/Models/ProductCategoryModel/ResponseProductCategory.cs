namespace JSSATSProject.Service.Models.ProductCategoryModel;

public class ResponseProductCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TypeId { get; set; }

    public string? Status { get; set; }
}