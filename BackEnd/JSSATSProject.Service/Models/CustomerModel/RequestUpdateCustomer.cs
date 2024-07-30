using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.CustomerModel;

public class RequestUpdateCustomer
{
    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    [Phone(ErrorMessage = "{0} must be a valid phone number." )]
    public string Phone { get; set; } = null!;

    [EmailAddress(ErrorMessage = "{0} must be a valid email address.")]
    public string Email { get; set; } = null!;
    
    [RegularExpression("(?i)(male|female)", ErrorMessage = "{0} must be either 'male' or 'female'.")]
    public string? Gender { get; set; }

    public string? Address { get; set; }
}