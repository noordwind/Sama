using System;
using System.Net;
using System.Threading.Tasks;
using Sama.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Sama.Core.Domain;

namespace Sama.Api.Framework
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private static Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            //var message = "There was an error.";
            var message = exception.Message;
            switch(exception)
            {
                case DomainException e:
                    errorCode = e.Code;
                    message = e.Message;
                    break;
                case ServiceException e:
                    errorCode = e.Code;
                    message = e.Message;
                    break;
            }
            var response = new { code = errorCode, message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}