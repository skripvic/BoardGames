using DataAccess;
using BuisinessLogic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BuisinessLogic.Settings;
using Microsoft.AspNetCore.Identity;
using DomainLayer.Entities;
using Presentation.Swagger;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

var appSettings = builder.Configuration.Get<AppSettings>()
                     ?? throw new NullReferenceException("Не заданы настройки приложения");


builder.Services.AddSingleton(appSettings);

builder.Services.AddDatabase(appSettings.Database);

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
    builder =>
    {
        builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
    });
});

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
    appSettings.Auth.SecretKey ?? throw new NullReferenceException("Не задан секретный ключ")));

builder.Services.AddAuth(signingKey);

builder.Services.AddIdentityCore<User>(o =>
    {
        o.Password.RequireDigit = true;
        o.Password.RequireLowercase = true;
        o.Password.RequireUppercase = false;
        o.Password.RequireNonAlphanumeric = false;
        o.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSwagger();


builder.Services.AddApplication(appSettings.Auth);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

