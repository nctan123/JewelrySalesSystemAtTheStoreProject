using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.StaffModel;

public class RequestUpdateStaff
{
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    [Phone]
    public string Phone { get; set; } = null!;
    [EmailAddress]
    public string Email { get; set; } = null!;

    public string? Address { get; set; }

    public string? Gender { get; set; }

    public string? Status { get; set; }
}