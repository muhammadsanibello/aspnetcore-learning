using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Interfaces;
using TaskManagerApi.Middleware;
using TaskManagerApi.Services;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs/app.log").CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<TaskManagerDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService,  ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
