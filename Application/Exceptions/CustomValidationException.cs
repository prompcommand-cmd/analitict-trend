using System.ComponentModel.DataAnnotations;

namespace Application.Exceptions
{
    public class CustomValidationException : Exception
    {

        private ValidationResult? _validationResult;
        public ValidationResult ValidationResult =>
            _validationResult ??= new ValidationResult(Message);
        public CustomValidationException(string errorMessage) : base(errorMessage)
        {
        }
        public CustomValidationException(string errorMessage, object? objectValue) : base(errorMessage)
        {
            Value = objectValue;
        }
        public object? Value { get; }
    }
}
