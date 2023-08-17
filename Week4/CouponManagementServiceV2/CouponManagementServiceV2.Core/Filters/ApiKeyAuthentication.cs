using CouponManagementServiceV2.Core.Model.Shared;
using Microsoft.AspNetCore.Http.Extensions;

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
            if (!context.Request.GetDisplayUrl().Contains("metrics"))
            {
                if (!context.Request.Headers.TryGetValue("x-api-key", out var apiKey))
                {
                    context.Response.StatusCode = 404;
                    return;
                }

                if (apiKey != _apiKey)
                {
                    context.Response.StatusCode = 404;

                    return;
                }
            }
                

            await _next(context);
        }
    }

    public class TokenAuthentication
    {
        private readonly RequestDelegate _next;
        private readonly Cryption _crypte;
        private readonly IConfiguration _configuration;


        public TokenAuthentication(RequestDelegate next, IConfiguration configuration)
        {
            _crypte = new Cryption();
            _configuration = configuration;
            _next = next;
     
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.GetDisplayUrl().Contains("Auth") && !context.Request.GetDisplayUrl().Contains("metrics"))
            {
                if (!context.Request.Headers.TryGetValue("token", out var token))
                {
                    context.Response.StatusCode = 401;
                    return;
                    
                }
                else
                {
                    try
                    {
                        int uid = _crypte.GetUserIdFromToken(token, _configuration["Jwt:Key"], _configuration["Jwt:Audience"], _configuration["Jwt:Issuer"]);
                        if (uid == -1)
                        {
                            context.Response.StatusCode = 401;
                           
                            return;
                        }
                    }
                    catch
                    {
                        context.Response.StatusCode = 401;
       
                        return;
                    }
                }
            }
            
            
            await _next(context);
        }
    }
}
