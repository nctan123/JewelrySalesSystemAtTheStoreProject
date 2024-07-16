namespace JSSATSProject.Service.Models.AccountModel;

public class RequestSignIn
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}