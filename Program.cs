using Microsoft.OpenApi.Models;
using Task_Management.Repositories;
using Task_Management.Data;

var builder = WebApplication.CreateBuilder(args);
// ✅ Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // your Angular app URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Task Management API MY",
        Version = "v1"
    });
});

// Dependency Injection
builder.Services.AddSingleton<DbHelper>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TaskRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1");
    });
}


// ✅ Enable CORS before controllers
app.UseCors("AllowAngularClient");


app.UseHttpsRedirection();
app.MapControllers();
app.Run();
