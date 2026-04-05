using Domain.Exceptions;
using Shared.ErrorModels;

namespace Store.G02.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails() 
                    { ErrorMessage = $"End Point {context.Request.Path} Not Found",StatusCode = StatusCodes.Status404NotFound};

                    await context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
                
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails() 
                { ErrorMessage = ex.Message};

                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException=> StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = response.StatusCode;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
