using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security;

namespace FitnessTracker.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await HandleExceptionAsync(context, ex.Errors);
            }

            catch (OperationCanceledException ex)
            {
                context.Response.StatusCode = 499;
                await HandleExceptionAsync(context, ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await HandleExceptionAsync(context, ex.Message);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, IEnumerable<FluentValidation.Results.ValidationFailure> errors)
        {
            return context.Response.WriteAsJsonAsync(new
            {
                Errors = errors
                .Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage,
                }),
                StatusCode = context.Response.StatusCode,
                TimeStamp = DateTime.UtcNow
            });
        }

        private static Task HandleExceptionAsync(HttpContext context, string message)
        {
            return context.Response.WriteAsJsonAsync(new
            {
                ErrorMessage = message,
                StatusCode = context.Response.StatusCode,
                TimeStamp = DateTime.UtcNow
            });
        }
    }
}
