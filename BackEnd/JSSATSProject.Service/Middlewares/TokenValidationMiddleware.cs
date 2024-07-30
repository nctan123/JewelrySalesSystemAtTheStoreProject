using JSSATSProject.Service.Service.IService;
using Microsoft.AspNetCore.Http;

namespace JSSATSProject.Service.Middlewares;

public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IActiveJWTService _activeJwtService;

    public TokenValidationMiddleware(RequestDelegate next, IActiveJWTService activeJwtService)
    {
        _next = next;
        _activeJwtService = activeJwtService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            var tokenValue = token.ToString().Replace("Bearer ", string.Empty);

            if (!await IsTokenValidAsync(tokenValue))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("This token has been expired.");
                return;
            }
        }
        await _next(context);
    }

    private async Task<bool> IsTokenValidAsync(string token)
    {
        return await _activeJwtService.IsValidTokenAsync(token);
    }
}