using CouponManagementService.Core;
using CouponManagementService.Core.Filters;
using CouponManagementService.Core.Models;
using CouponManagementService.WebApi.Models;
using FluentValidation.AspNetCore;
using NLog.Extensions.Logging;
using NLog.Web;


var builder = WebApplication.CreateBuilder(args);
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserValidator>());
    builder.Services.AddSingleton<DatabaseConfig>();


    builder.Services.AddLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddNLog();
        logging.SetMinimumLevel(LogLevel.Information);
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    var apiKey = "12345";

    app.UseMiddleware<ApiKeyAuthentication>(apiKey);

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}


