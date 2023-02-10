using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Redirect.Application.Commands.GenerateNewShortenedUrl;
using Redirect.Application.Services;
using Redirect.Core.Repositories;
using Redirect.Core.Services;
using Redirect.Infrastructure.Persistence;
using Redirect.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IRedirectRepository, RedirectRepository>();

builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
builder.Services.AddScoped<IExpirationCheckerService, ExpirationCheckerService>();

builder.Services.AddMediatR(typeof(GenerateNewShortenedUrlCommand));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Redirect.API",
        Version = "v1",
        Description = "Repository developed to practice knowledge in ASP .NET Core using C#, .NET 6, Clean Architecture, CQRS, Entity Framework Core, Dapper and Repository Pattern. This project is a URL shortener and uses PostgreSQL as a database."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddHttpContextAccessor();

var connectionString = builder.Configuration.GetConnectionString("RedirectCs");

builder.Services.AddDbContext<RedirectDbContext>(options => options.UseNpgsql(connectionString));

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
