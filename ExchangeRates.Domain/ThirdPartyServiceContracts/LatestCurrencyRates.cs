namespace ExchangeRates.Domain.ThirdPartyServiceContracts
{
    public class LatestCurrencyRates
    {
        public string Base { get; set; }

        public Dictionary<string, string> Rates { get; set; }
    }
}
