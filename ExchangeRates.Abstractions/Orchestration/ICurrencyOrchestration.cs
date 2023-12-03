using ExchangeRates.Contracts;

namespace ExchangeRates.Abstractions.Orchestration
{
    public interface ICurrencyOrchestration
    {
        Task<IReadOnlyCollection<Currency>> GetAsync();

        Task<double> ConvertAsync(double value, string from, string to);

        Task<CurrencyRate> GetLatestAsync();
    }
}
