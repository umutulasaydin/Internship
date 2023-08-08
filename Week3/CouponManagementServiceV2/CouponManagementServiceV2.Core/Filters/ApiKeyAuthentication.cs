namespace CouponManagementServiceV2.Core.Filters
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
                
                return;
            }

            if (apiKey != _apiKey)
            {
                context.Response.StatusCode = 401;
                
                return;
            }

            await _next(context);
        }
    }
}
