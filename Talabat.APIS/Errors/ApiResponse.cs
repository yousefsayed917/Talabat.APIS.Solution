
namespace Talabat.APIS.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int _statuscode, string? _message = null)
        {
            StatusCode = _statuscode;
            Message = _message ?? GetDefaultMessageForStatusCode(StatusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return StatusCode switch
            {
                400 => "Bad Request",
                401 => "Un Authorize",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => null
            };
        }

    }
}
