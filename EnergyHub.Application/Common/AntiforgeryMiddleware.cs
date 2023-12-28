using Azure;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

public class AntiforgeryMiddleware : IMiddleware
{
    private readonly IAntiforgery _antiforgery;

    public AntiforgeryMiddleware(IAntiforgery antiforgery)
    {
        _antiforgery = antiforgery ?? throw new ArgumentNullException(nameof(antiforgery));
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (next == null)
        {
            throw new ArgumentNullException(nameof(next));
        }

        // Skip anti-forgery validation for GET requests
        if (string.Equals("GET", context.Request.Method, StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }
        try
        {
            // Generate and validate the CSRF token

            //_antiforgery.SetCookieTokenAndHeader(context);
            
            
            var tokens = _antiforgery.GetAndStoreTokens(context);
            context.Request.Headers.Append("CSRF-TOKEN", tokens.RequestToken);


            //Response.Cookies.Append("CSRF-TOKEN", headerToken, new CookieOptions
            //{
            //    HttpOnly = false
            //});


            await _antiforgery.ValidateRequestAsync(context);


            // Add the token to the response headers
            await next(context);


        }
        catch (AntiforgeryValidationException)
        {
            // Handle the case where the anti-forgery validation fails
            context.Response.StatusCode = 403; // Forbidden
            return;
        }

        await next(context);
    }
}
