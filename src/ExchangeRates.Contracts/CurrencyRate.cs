using System.Collections.Generic;

namespace ExchangeRates.Contracts
{
    public class CurrencyRate
    {
        public string Base { get; set; }

        public IReadOnlyCollection<Rate> Rates { get; set; }
    }
}
