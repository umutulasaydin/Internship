using Microsoft.Extensions.Caching.Memory;


namespace CouponManagementServiceV2.Core.Filters
{
    public class LimitLog
    {
        public int unauth { get; set; } = 0;
        public int notfound { get; set; } = 0;
    }
  
    public class RateLimitter
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public RateLimitter(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            var address = context.Connection.RemoteIpAddress.ToString();
            if (context.Request.Method == "OPTIONS")
            {
                await _next(context);
                return;
            }
            if  (!_cache.TryGetValue(address, out LimitLog request))
            {
                await _next(context);
                request = new LimitLog();
                if (context.Response.StatusCode == 401)
                {
                    request.unauth = 1;
                    _cache.Set(address, request, TimeSpan.FromMinutes(5));
                    
                }
                else if (context.Response.StatusCode == 404)
                {
                    request.notfound = 1;
                    _cache.Set(address, request, TimeSpan.FromHours(1));

                }
            }
            else
            {
                if (request.unauth == 3)
                {
                    
                    context.Response.StatusCode = 429;
                    return;
                }
                else if (request.notfound == 2)
                {
                    context.Response.StatusCode = 403;
                }
                else if (request.unauth < 3 && request.notfound < 2)
                {
                    await _next(context);
                    
                    if (context.Response.StatusCode == 401)
                    {
                        request.unauth += 1;
                        if (request.unauth == 3)
                        {
                            _cache.Set(address, request, TimeSpan.FromHours(1));
                        }
                        else
                        {
                            _cache.Set(address, request, TimeSpan.FromMinutes(5));
                        }
                    }

                    else if (context.Response.StatusCode == 404)
                    {
                        request.notfound += 1;
                        if (request.notfound == 2)
                        {
                            _cache.Set(address, request, TimeSpan.FromDays(1));
                        }
                        else
                        {
                            _cache.Set(address, request, TimeSpan.FromHours(1));
                        }
                    }

                }
            }
        }
    }
}
