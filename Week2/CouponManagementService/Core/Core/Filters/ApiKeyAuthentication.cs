namespace CouponManagementService.Core.Filters
{
    public class ApiKeyAuthentication
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyAuthentication(RequestDelegate next, string apiKey)
        {
            _next = next;
            _apiKey = apiKey;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("x-api-key", out var apiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API key not found in request headers.");
                return;
            }

            if (apiKey != _apiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid API key.");
                return;
            }

            await _next(context);
        }
    }
}
