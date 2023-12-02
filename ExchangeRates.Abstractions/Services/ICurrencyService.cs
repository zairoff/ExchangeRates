using ExchangeRates.Contracts;

namespace ExchangeRates.Abstractions.Services
{
    public interface ICurrencyService
    {
        Task<IReadOnlyCollection<Currency>> GetAsync();

        Task<IReadOnlyCollection<Currency>> GetLatestAsync();

        Task<double> ConvertAsync(double value, string from, string to);
    }
}
