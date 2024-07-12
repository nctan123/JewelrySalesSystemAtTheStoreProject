using JSSATSProject.Repository.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Service.Models.StaffModel;

public class RequestCreateStaff
{
    public int AccountId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    [VietnamesePhone(ErrorMessage = "{0} must be a valid Vietnamese phone number.")]
    public string Phone { get; set; } = null!;

    [EmailAddress(ErrorMessage = "{0} must be a valid email address.")]
    public string Email { get; set; } = null!;

    public string? Address { get; set; }
    [RegularExpression("(?i)(male|female)", ErrorMessage = "{0} must be either 'male' or 'female'.")]
    public string? Gender { get; set; }

}