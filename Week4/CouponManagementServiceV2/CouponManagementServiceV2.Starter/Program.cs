using CouponManagementServiceV2.Core.Filters;
using CouponManagementServiceV2.Core.Model.Shared;
using CouponManagementServiceV2.Core.Model.Data;
using FluentValidation.AspNetCore;
using NLog.Web;
using NLog.Extensions.Logging;
using CouponManagementServiceV2.Core.Business.Interfaces;
using CouponManagementServiceV2.Core.Business.Services;
using CouponManagementServiceV2.Core.Data.Interfaces;
using CouponManagementServiceV2.Core.Data.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NLog.Web.LayoutRenderers;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using App.Metrics.AspNetCore;
using App.Metrics.Formatters.Prometheus;

var builder = WebApplication.CreateBuilder(args);
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
// Add services to the container.

        

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DatabaseConfig>();

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<BaseRequestValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CouponSeriesValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CouponValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CouponLogValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RedemptCouponValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<StatusCouponValidator>());

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddNLog();
});




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddTransient<ICommandRepository, CommandRepository>();
builder.Services.AddTransient<IQueryRepository, QueryRepository>();

builder.Services.AddScoped<ICommandService, CommandService>();
builder.Services.AddScoped<IQueryService, QueryService>();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
}
);
builder.Services.AddMetrics();

builder.Host.UseMetricsWebTracking();
builder.Host.UseMetrics(options => {
    options.EndpointOptions = endpointOptions =>
    {
        endpointOptions.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
        endpointOptions.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
       
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<ApiKeyAuthentication>(builder.Configuration["X-Api-Key"]);
app.UseMiddleware<TokenAuthentication>();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
