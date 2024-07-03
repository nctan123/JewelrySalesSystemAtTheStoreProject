namespace JSSATSProject.Service.Models.CustomerModel;

public class RequestCreateCustomer
{
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Gender { get; set; }

    public string? Address { get; set; }
}