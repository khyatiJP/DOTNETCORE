using Microsoft.AspNetCore.Authorization;
using Theatre.API.Handler;

namespace Theatre.API.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration; ;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var endpoint = context.GetEndpoint();
                // await _next(context);
                ///return;

                if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
                {
                    await _next(context);
                    return;
                }
                string token = string.Empty;
                string issuer = _configuration["Jwt:Issuer"]; //Get issuer value from your configuration
                string audience = _configuration["Jwt:Audience"]; //Get audience value from your configuration
                string key = _configuration["Jwt:Key"];
                string metaDataAddress = issuer + "/.well-known/oauth-authorization-server";
                AuthHandler authHandler = new AuthHandler();
                var header = context.Request.Headers["Authorization"];
                if (header.Count == 0)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
                string[] tokenValue = Convert.ToString(header).Trim().Split(" ");
                if (tokenValue.Length > 1) token = tokenValue[1];
                else context.Response.StatusCode = StatusCodes.Status400BadRequest;//throw new Exception("Authorization token is empty");
                if (authHandler.IsValidToken(token, issuer, audience, key) == 200) await _next(context);
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                /// HttpResponseWritingExtensions.WriteAsync(context.Response, "{\"message\": \"Unauthorized\"}").Wait();
            }
            return;
        }
    }
}
