using JSSATSProject.Repository.Entities;

namespace JSSATSProject.Service.Models.NewFolder;

public class ResponseOrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }
    public string ProductName { get; set; }

    public string Size { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal SizePrice { get; set; }

    public string Status { get; set; }

    public Product Product { get; set; }
}