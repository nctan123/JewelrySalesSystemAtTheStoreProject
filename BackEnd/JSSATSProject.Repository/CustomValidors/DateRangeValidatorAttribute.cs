using JSSATSProject.Repository.CustomValidors;
using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Repository.CustomValidators
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Ensure the object instance is the correct type
            if (validationContext.ObjectInstance is not IDateRange dto)
            {
                return new ValidationResult("Invalid object type for date range validation.");
            }

            // Extract date values
            var startDate = dto.StartDate;
            var endDate = dto.EndDate;

            // Validate dates
            if (startDate < DateTime.Now)
            {
                return new ValidationResult("StartDate must be greater than or equal to the current date.");
            }

            if (endDate <= startDate)
            {
                return new ValidationResult("EndDate must be greater than StartDate.");
            }

            return ValidationResult.Success;
        }

    }
}