using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CaseStudyAppServer.Middlewares
{
    public class UnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UnauthorizedMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var authorizeData = endpoint?.Metadata.GetMetadata<IAuthorizeData>();

            if (authorizeData != null && context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                var authHeaderValue = authorizationHeader.ToString();
                if (authHeaderValue.StartsWith("Bearer ", System.StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeaderValue.Substring("Bearer ".Length).Trim();

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var _tokenRepo = scope.ServiceProvider.GetRequiredService<ITokenRepository>();

                        // Validate the token
                        var isValidToken = await _tokenRepo.ValidateAsync(token);
                        if (isValidToken == false)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsJsonAsync(
                                new
                                {
                                    Status = false,
                                    Message = MessageConstants.UnknownToken,
                                }
                            );
                            return;
                        }
                        else if (isValidToken == null)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsJsonAsync(
                                new
                                {
                                    Refresh = true,
                                    Status = false,
                                    Message = MessageConstants.RefreshToken,
                                }
                            );
                            return;
                        }
                    }
                }
            }

            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        Status = false,
                        Message = MessageConstants.UnknownToken,
                    }
                );
            }
        }
    }
}
