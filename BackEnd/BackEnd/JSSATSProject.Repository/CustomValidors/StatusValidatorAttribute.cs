using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Repository.CustomValidators;

public class StatusValidatorAttribute : ValidationAttribute
{
    private readonly string[] _validStatuses;

    public StatusValidatorAttribute()
    {
        _validStatuses = [];
    }
    public StatusValidatorAttribute(params string[] validStatuses)
    {
        _validStatuses = validStatuses;
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string)
        {
            return new ValidationResult("Invalid status value.");
        }

        string status = (value as string)!;
        if (Array.Exists(_validStatuses, s => s.Equals(status, StringComparison.OrdinalIgnoreCase)))
        {
            return ValidationResult.Success!;
        }

        return new ValidationResult($"Status must be one of the following values: {string.Join(", ", _validStatuses)}.");
    }
}