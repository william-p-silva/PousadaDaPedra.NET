using PousadaDaPedra.Application.DTOs.ResponseDTO;

namespace PousadaDaPedra.WebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ArgumentException ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var dtoErro = new ErrorApiDTO()
            {
                Message = ex.Message,
                Status = httpContext.Response.StatusCode,
                success = false,
            };
            await httpContext.Response.WriteAsJsonAsync(dtoErro);
        }
        catch (UnauthorizedAccessException ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            var dtoErro = new ErrorApiDTO()
            {
                Message = ex.Message,
                Status = httpContext.Response.StatusCode,
                success = false,
            };

            await httpContext.Response.WriteAsJsonAsync(dtoErro);
        }
        catch (Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var dtoErro = new ErrorApiDTO()
            {
                Message = $"Erro interno no servidor {ex.Message}",
                Status = httpContext.Response.StatusCode,
                success = false,
            };
            
            await httpContext.Response.WriteAsJsonAsync(dtoErro);
        }
    }
}