using ExchangeRates.Contracts;

namespace ExchangeRates.Services.Mappers
{
    public static class CurrencyMapper
    {
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
    }
}
