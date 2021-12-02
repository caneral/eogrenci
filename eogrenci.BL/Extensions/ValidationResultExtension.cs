using System;
using System.Collections.Generic;
using Core.ResponseObjects;
using FluentValidation.Results;

namespace eogrenci.BL.Extensions
{
    public static class ValidationResultExtension
    {
        public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult validationResult)
        {
            List<CustomValidationError> errors = new();

            foreach (var error in validationResult.Errors)
            {
                errors.Add(new CustomValidationError()
                {
                    ErrorMessage = error.ErrorMessage,
                    PropertyName = error.PropertyName

                });
            }
            return errors;
        }
    }
}
