using ExchangeRates.Abstractions.Orchestration;
using ExchangeRates.Abstractions.Repositories;
using ExchangeRates.Abstractions.Services;
using ExchangeRates.Contracts;
using ExchangeRates.Domain.Models;
using ExchangeRates.Services.Mappers;

namespace ExchangeRates.Api.Orchestration
{
    public class CurrencyOrchestration : ICurrencyOrchestration
    {
        private readonly string BaseCurrency = "USD";
        private readonly ICurrencyService _currencyService;
        private readonly IRepository<ExchangeRate> _repository;

        public CurrencyOrchestration(IRepository<ExchangeRate> repository, ICurrencyService currencyService)
        {
            _repository = repository;
            _currencyService = currencyService;
        }

        public Task<double> ConvertAsync(double value, string from, string to)
        {
            return _currencyService.ConvertAsync(value, from, to);
        }

        public async Task<CurrencyRate> GetLatestAsync()
        {
            ExchangeRate exchangeRate = await _repository.FindAsync(x => x.Date.Date == DateTime.Now.Date);

            if (exchangeRate != null)
            {
                return CurrencyMapper.MapCurrencyRates(exchangeRate.Rates);
            }

            Dictionary<string, string> currencyRate = await _currencyService.GetLatestRateAsync();

            if (currencyRate != null)
            {
                exchangeRate = new ExchangeRate
                {
                    Base = BaseCurrency,
                    Rates = currencyRate,
                    Date = DateTime.Now,
                };

                await _repository.AddAsync(exchangeRate);
            }

            return CurrencyMapper.MapCurrencyRates(currencyRate);
        }

        public Task<IReadOnlyCollection<Currency>> GetAsync()
        {
            return _currencyService.GetAsync();
        }
    }
}
