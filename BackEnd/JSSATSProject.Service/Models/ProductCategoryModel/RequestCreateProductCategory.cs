namespace JSSATSProject.Service.Models.ProductCategoryModel;

public class RequestCreateProductCategory
{
    public string Name { get; set; } = null!;

    public int TypeId { get; set; }

    public string? Status { get; set; }
}