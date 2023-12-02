using ExchangeRates.Abstractions.Repositories;
using ExchangeRates.Abstractions.Services;
using ExchangeRates.Repositories;
using ExchangeRates.Services;
using Microsoft.EntityFrameworkCore;

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

    services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(configuration.GetConnectionString("Connection")));

    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
}
