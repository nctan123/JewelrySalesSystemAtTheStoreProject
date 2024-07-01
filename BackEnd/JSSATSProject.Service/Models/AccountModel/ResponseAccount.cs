namespace JSSATSProject.Service.Models.AccountModel;

public class ResponseAccount
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Status { get; set; }

    public string Role { get; set; } = null!;

    public string StaffName { get; set; }
}