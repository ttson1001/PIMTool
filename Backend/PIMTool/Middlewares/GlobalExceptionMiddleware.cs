using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PIMTool.Core.Exceptions.Employee;
using PIMTool.Core.Exceptions.Group;
using PIMTool.Core.Exceptions.Project;

namespace PIMTool.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await PareExceptionAsync(ex, context);
            }

        }

        private async Task PareExceptionAsync(Exception ex, HttpContext context)
        {
            int statusCode = 500;
            string Error = "Internal Server Error";
            if (ex is EmployeeNotFoundException ||
                ex is BirthDayException ||
                ex is EmployeeDuplicateVisaException ||
                ex is GroupNotFoundException ||
                ex is ProjectNotFoundException)
            {
                statusCode = 400;
                Error = "Bad Request";
            }
            context.Response.StatusCode = statusCode;
            _logger.LogError(ex, "Unexpected exception");
            var response = new
            {
                StatusCode = statusCode,
                error = Error,
                message = ex.Message
            };
            var json = JsonConvert.SerializeObject(response);
            await context.Response.WriteAsync(json);
        }

    }
}