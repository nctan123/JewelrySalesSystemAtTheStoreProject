using System;
using System.ComponentModel.DataAnnotations;

namespace JSSATSProject.Repository.CustomValidators
{
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        private DateTime StartDate { get; set; }
        private DateTime EndDate { get; set; }

        public DateRangeValidatorAttribute()
        {
            StartDate = DateTime.Parse("01/01/1900");
            EndDate = DateTime.Now;
            ErrorMessage = "{0} must be between " + StartDate.ToShortDateString() + " and today.";
        }

        public DateRangeValidatorAttribute(DateTime endDate)
        {
            EndDate = endDate;
        }

        public DateRangeValidatorAttribute(string startDate, string? endDate = null)
        {
            StartDate = DateTime.Parse(startDate);
            EndDate = DateTime.Now;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var dateTimeValue = DateTime.Now;
            if (StartDate <= dateTimeValue && dateTimeValue <= EndDate)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ??
                                        $"{validationContext.DisplayName} must be between {StartDate.ToShortDateString()} and {EndDate.ToShortDateString()}.");
        }
    }
}