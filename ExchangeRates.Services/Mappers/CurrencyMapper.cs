using ExchangeRates.Contracts;

namespace ExchangeRates.Services.Mappers
{
    public static class CurrencyMapper
    {
        private const string BaseCurrency = "USD";

        public static IReadOnlyCollection<Currency> MapCurrencies(Dictionary<string, string> incomingCurrencies)
        {
            if (incomingCurrencies == null || incomingCurrencies.Count < 1)
            {
                return new List<Currency>(0);
            }

            var currencies = new List<Currency>(incomingCurrencies.Count);
            foreach (var keyValuePair in incomingCurrencies)
            {
                var currency = new Currency
                {
                    Code = keyValuePair.Key,
                    Country = keyValuePair.Value,
                };

                currencies.Add(currency);
            }

            return currencies;
        }

        public static CurrencyRate MapCurrencyRates(Dictionary<string, string> incomingRates)
        {
            if (incomingRates == null || incomingRates.Count < 1)
            {
                return new CurrencyRate();
            }

            var rates = new List<Rate>(incomingRates.Count);
            foreach (var keyValuePair in incomingRates)
            {
                var rate = new Rate
                {
                    CountryCode = keyValuePair.Key,
                    Value = keyValuePair.Value,
                };

                rates.Add(rate);
            }

            return new CurrencyRate { Base = BaseCurrency, Rates = rates };
        }
    }
}
