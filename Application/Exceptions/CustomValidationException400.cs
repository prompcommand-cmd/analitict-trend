using System.ComponentModel.DataAnnotations;

namespace Application.Exceptions
{
    public class CustomValidationException400 : Exception
    {

        private ValidationResult? _validationResult;
        public ValidationResult ValidationResult =>
            _validationResult ??= new ValidationResult(Message);
        public CustomValidationException400(string errorMessage) : base(errorMessage)
        {
        }
        public CustomValidationException400((string errorCategory, string errorMessage) error) : base(error.errorMessage)
        {
            ErrorCategory = error.errorCategory;
        }
        public CustomValidationException400(string errorMessage, object? objectValue) : base(errorMessage)
        {
            Value = objectValue;
        }
        public CustomValidationException400((string errorCategory, string errorMessage) error, object? objectValue) : base(error.errorMessage)
        {
            ErrorCategory = error.errorCategory;
            Value = objectValue;
        }
        public object? Value { get; }

        public string? ErrorCategory { get; }
    }
}
