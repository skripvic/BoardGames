using Microsoft.EntityFrameworkCore;
using DataAccess;
using BuisinessLogic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

builder.Services.AddApplication();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .Build();

//builder.Services.AddDbContext<SystemDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddScoped<ISystemDbContext>(isp => isp.GetRequiredService<SystemDbContext>());

builder.Services.AddDatabase(configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

