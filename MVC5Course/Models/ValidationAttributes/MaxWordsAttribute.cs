using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ValidationAttributes
{
    public class MaxWordsAttribute : ValidationAttribute
    {
        private readonly int MaxWords;
        public MaxWordsAttribute(int maxWords) : base("{0} 字數過長 ")
        {
            MaxWords = maxWords;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null) {
                var valueAsString = value.ToString();
                if (valueAsString.Split(' ').Length > MaxWords) {
                    var errorMessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errorMessage);
                }
            }
            return ValidationResult.Success;
        }
    }
}