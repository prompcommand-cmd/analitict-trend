namespace WebApi.Models
{
    public class ResponseBuilder<T>
    {
        public ErrorDetail Error { get; set; } = new ErrorDetail();
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
