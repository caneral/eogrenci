using System;
namespace Core.ResponseObjects
{
    public class CustomValidationError
    {
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }
}
