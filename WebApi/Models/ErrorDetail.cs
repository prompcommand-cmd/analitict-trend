namespace WebApi.Models
{
    public class ErrorDetail
    {
        public bool IsError { get; set; } = false;
        public string? ErrorType { get; set; }
        public string? ErrorCategory { get; set; }
        public string? ErrorId { get; set; }
        public List<string>? ErrorMessages { get; set; } = new List<string>();
        public object? ErrorData { get; set; }
    }
}
