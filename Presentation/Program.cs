using DataAccess;
using BuisinessLogic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BuisinessLogic.Settings;
using Microsoft.AspNetCore.Identity;
using DomainLayer.Entities;

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

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddApplication(appSettings.Auth);

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

