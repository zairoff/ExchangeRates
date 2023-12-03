using ExchangeRates.Abstractions.Orchestration;
using ExchangeRates.Abstractions.Repositories;
using ExchangeRates.Abstractions.Services;
using ExchangeRates.Api.Orchestration;
using ExchangeRates.Repositories;
using ExchangeRates.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ExchangeRates.Contracts;
using ExchangeRates.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

RegisterServices(builder.Services, builder.Configuration);

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

void RegisterServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddHttpClient<ICurrencyService, CurrencyService>();
    services.AddScoped<ICurrencyOrchestration, CurrencyOrchestration>();

    services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(configuration.GetSection("Database:SQL:Connection").Value));

    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    services.AddFluentValidationAutoValidation();
    services.AddFluentValidationClientsideAdapters();
    services.AddTransient<IValidator<ConvertArgs>, ConvertArgsValidator>();
}
