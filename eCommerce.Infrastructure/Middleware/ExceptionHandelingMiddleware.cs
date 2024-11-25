using eCommerce.Application.Services.Interfaces.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure.Middleware
{
    public class ExceptionHandelingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (DbUpdateException ex) 
            {
                var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandelingMiddleware>>();
                context.Response.ContentType = "application/json";
                if (ex.InnerException is SqlException innerException)
                {
                    logger.LogError(innerException, "Sql Exception");
                    switch (innerException.Number)
                    {
                        case 2627:
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Unique constraint violation");
                            break;
                        case 515:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsync("Can not insert null");
                            break;
                    }
                }
                else 
                {
                    logger.LogError(ex, "Related Ef exception");
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Error occuared");
                }
            }
            catch(Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"Error: {ex.Message}");

            }
        }
    }
}
