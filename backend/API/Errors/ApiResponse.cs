using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

       
        public int StatusCode { get; set; }
        public string Message { get; set; }

         private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch {
                 400 => "A Bad Request ",
                 401 => "Uathorized, Request",
                 404 => "Ressource Not Found",
                 500 => "Error On The Server",
                 _ => null
            };
        }
    }
}