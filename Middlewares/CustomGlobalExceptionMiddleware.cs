using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using PropertyAPI.CustomModels;

namespace PropertyAPI.Middlewares
{
    public class CustomGlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomGlobalExceptionMiddleware> logger;
        private readonly IWebHostEnvironment env;

        public CustomGlobalExceptionMiddleware(
            RequestDelegate next, 
            ILogger<CustomGlobalExceptionMiddleware> logger,
            IWebHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch(Exception ex)
            {
                ApiResponse apiResponse;
                HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

                int statusCode = GetStatusCodeFromException(ex);

                if (env.IsDevelopment())
                {
                    apiResponse = new ApiResponse(statusCode, ex.Message, ex.StackTrace);
                }
                else
                {
                    apiResponse = new ApiResponse((int)httpStatusCode, ex.Message);
                }

                logger.LogError(ex, ex.Message);
                httpContext.Response.StatusCode = statusCode;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(apiResponse.ToString());
            }
        }

        private int GetStatusCodeFromException(Exception ex)
        {
            var exceptionType = ex.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                return (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}