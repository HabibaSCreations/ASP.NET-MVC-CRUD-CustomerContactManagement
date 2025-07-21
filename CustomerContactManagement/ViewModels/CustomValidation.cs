using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerContactManagement.ViewModels
{
    public class CustomeDateTimeValidation : ValidationAttribute
    {
        public DateTime MinimumDate { get; set; }
        public DateTime MaximumDate { get; set; }
        public CustomeDateTimeValidation(string minimumDate, string maximumDate)
        {
            MinimumDate = DateTime.Parse(minimumDate);
            MaximumDate = DateTime.Parse(maximumDate);
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTimeValue)
            {
                if (dateTimeValue < MinimumDate || dateTimeValue > MaximumDate)
                {
                    return new ValidationResult(ErrorMessage ?? $"Date must be between {MinimumDate:d} and {MaximumDate:d}.");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Values is not a valid date.");
        }
    }
}