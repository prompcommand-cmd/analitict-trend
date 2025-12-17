using WebApi.Models;

namespace WebApi.Utilities
{
    public static class ResponseUtlity
    {
        public static ResponseBuilder<T> ToStandardResponse<T>(this T response)
        {
            return new ResponseBuilder<T>
            {
                Data = response,
            };
        }
        public static ResponseBuilder<T> ToStandardResponse<T>(this T response, string message)
        {
            return new ResponseBuilder<T>
            {
                Message = message,
                Data = response,
            };
        }

        public static ResponseBuilder<T> ToStandardResponse<T>(this T response, ErrorDetail error)
        {
            return new ResponseBuilder<T>
            {
                Error = error,
                Data = response,
            };
        }
    }
}
