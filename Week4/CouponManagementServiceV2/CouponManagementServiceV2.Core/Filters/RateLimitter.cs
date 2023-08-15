using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace CouponManagementServiceV2.Core.Filters
{
  
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
            
            if  (!_cache.TryGetValue(address, out int request))
            {
                await _next(context);
                if (context.Response.StatusCode != 200)
                {
                    request = 1;
                    _cache.Set(address, request);
                    
                }
            }
            else
            {
                if (request == 3)
                {
                    context.Response.Clear();
                    context.Response.StatusCode = 429;
                    return;
                }
                else if (request < 3)
                {
                    await _next(context);
                    request += 1;
                    if (context.Response.StatusCode != 200)
                    {
                        _cache.Set(address, request, TimeSpan.FromHours(1));
                    }
                      

                }
            }
        }
    }
}
