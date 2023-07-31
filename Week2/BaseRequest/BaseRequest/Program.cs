using BaseRequest.Helpers;
using BaseRequest.BaseRequest;
using BaseRequest.Models;
using BaseRequest.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<BaseRequest<BaseResponse<Student>, Student>, StudentService>();
builder.Services.AddScoped<BaseRequest<BaseResponse<Teacher>, Teacher>, TeacherService>();
builder.Services.AddSingleton<ConnectionHelper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
