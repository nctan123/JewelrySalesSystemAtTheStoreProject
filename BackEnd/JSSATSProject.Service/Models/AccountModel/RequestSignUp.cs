namespace JSSATSProject.Service.Models.AccountModel;

public class RequestSignUp
{
    public int RoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}