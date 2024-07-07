using System.ComponentModel.DataAnnotations;
using JSSATSProject.Repository.CustomValidators;
using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JSSATSProject.Service.Models.CustomerModel;

public class ResponseCustomer
{
    public int Id { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;

    [Phone(ErrorMessage = "{0} must be a valid phone number." )]
    public string Phone { get; set; } = null!;

    [EmailAddress(ErrorMessage = "{0} must be a valid email address.")]
    public string Email { get; set; } = null!;
    
    [RegularExpression("(?i)(male|female)", ErrorMessage = "{0} must be either 'male' or 'female'.")]
    public string? Gender { get; set; }
    public string? Address { get; set; }
    
    public DateTime CreateDate { get; set; }

    public virtual Point Point { get; set; }
}