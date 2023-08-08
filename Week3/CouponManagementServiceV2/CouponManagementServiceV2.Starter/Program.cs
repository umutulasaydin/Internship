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

var builder = WebApplication.CreateBuilder(args);
var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DatabaseConfig>();

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<UserValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CouponSeriesValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CouponValidator>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CouponLogValidator>());

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
