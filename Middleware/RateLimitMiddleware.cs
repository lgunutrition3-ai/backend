using System.Collections.Concurrent;
using System.Net;

namespace Nutrition_backend.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, (int Count, DateTime Expiry)> _requestCounts = new();
        private readonly ILogger<RateLimitMiddleware> _logger;

        public RateLimitMiddleware(RequestDelegate next, ILogger<RateLimitMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Only apply to actual login POSTs, not CORS preflight (OPTIONS)
            if (context.Request.Method == "POST" && (context.Request.Path.StartsWithSegments("/api/auth/login") || context.Request.Path.StartsWithSegments("/api/auth/superadmin-login")))
            {
                var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var key = $"login_{ipAddress}";

                if (_requestCounts.TryGetValue(key, out var entry))
                {
                    if (entry.Expiry > DateTime.UtcNow)
                    {
                        if (entry.Count >= 5) // 5 attempts per 5 minutes
                        {
                            _logger.LogWarning($"Rate limit exceeded for IP: {ipAddress}");
                            context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                            await context.Response.WriteAsJsonAsync(new 
                            { 
                                message = "Too many login attempts. Please try again later.",
                                retryAfter = entry.Expiry - DateTime.UtcNow
                            });
                            return;
                        }
                        _requestCounts[key] = (entry.Count + 1, entry.Expiry);
                    }
                    else
                    {
                        _requestCounts[key] = (1, DateTime.UtcNow.AddMinutes(5));
                    }
                }
                else
                {
                    _requestCounts[key] = (1, DateTime.UtcNow.AddMinutes(5));
                }
            }

            await _next(context);
        }
    }
}