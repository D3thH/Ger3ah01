using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Error
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefalteMessageForstatusCode(StatusCode);
        }

        public int StatusCode{ get; set; }
        public String Message { get; set; }

           private string GetDefalteMessageForstatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, You have made",
                401 => "auth, You are not",
                404 => "Resurce Found, was Not",
                500 => "Internal server error",
                _ => null
            };
                
        }
    }
}