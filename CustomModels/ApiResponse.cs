using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PropertyAPI.CustomModels
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            
        }
        public ApiResponse(int code, string errorMessage, string? errorDetail = null)
        {
            Code = code;
            ErrorMessage = errorMessage;
            ErrorDetail = errorDetail;
        }

        public int Code {get; set;}
        public string ErrorMessage {get; set;} = string.Empty;
        public string? ErrorDetail {get; set;}

        public override string ToString()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}