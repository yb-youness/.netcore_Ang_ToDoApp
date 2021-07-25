using System.Net;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using API.Errors;
using System.Text.Json;

namespace API.Middleware
{
      //! Replace The Satrtup Class With This Midelwaree To use the Coustume Exseption 
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env ;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger ,IHostEnvironment env )
        {
             _next = next;
            _logger = logger;
            _env = env;
        }
       
        public async Task InvokeAsync(HttpContext context){
             try{
                 await _next(context);
          
             }catch(Exception ex){
               _logger.LogError(ex,ex.Message);
               context.Response.ContentType = "application/json"; // Set the Content Type
               context.Response.StatusCode = (int) HttpStatusCode.InternalServerError; // Send 500 Stauscode
                             //! If in Devlopement We Must Send A Detailed Descriptve Error Else in Production Mode Send Only Staus Code 
                var response = _env.IsDevelopment()? new ApiException((int) HttpStatusCode.InternalServerError,ex.Message
                 ,ex.StackTrace.ToString()): new ApiException((int) HttpStatusCode.InternalServerError);
                
                  //! This To Set Up The Responce Format To CamelCase 
                 var optionJson =new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                 
                 
                 var json = JsonSerializer.Serialize(response,optionJson); // Format The responce As Json Object 
                 await context.Response.WriteAsync(json); // Send The Object  As A Json responce 
                
             }
        }

      
    }
}