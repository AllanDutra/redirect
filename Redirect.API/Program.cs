using MediatR;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRedirectRepository, RedirectRepository>();

builder.Services.AddHostedService<ConsumeScopedServiceHostedService>();
builder.Services.AddScoped<IExpirationCheckerService, ExpirationCheckerService>();

builder.Services.AddMediatR(typeof(GenerateNewShortenedUrlCommand));

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
