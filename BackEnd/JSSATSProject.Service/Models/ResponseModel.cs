namespace JSSATSProject.Service.Models;

public class ResponseModel
{
    public int? TotalElements { get; set; }
    public int? TotalPages { get; set; }
    public string? MessageError { get; set; }
    public object? Data { get; set; }

    public int CalculateTotalPageCount(int pageSize)
    {
        return (int)Math.Ceiling((decimal)TotalElements! / pageSize);
    }
}