using Presentation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServices();

builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.Run();
