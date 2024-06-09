using FluentValidation;
using Microsoft.OpenApi.Models;
using Produces.AspNetCore.Extensions;
using Produces.Example.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProduces();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Produces.Example",
        Description = $"Started at: {DateTime.UtcNow}",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseEndpoint(typeof(Program).Assembly);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();