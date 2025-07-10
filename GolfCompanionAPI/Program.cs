using GolfCompanionAPI.Models;
using GolfCompanionAPI.Services;
using SharedGolfClasses;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<GolfCourseApiSettings>(
    builder.Configuration.GetSection("GolfCourseApi"));

builder.Services.AddHttpClient<GolfCourseService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
