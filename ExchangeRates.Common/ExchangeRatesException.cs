namespace ExchangeRates.Common
{
    public class ExchangeRatesException : Exception
    {
        public string ExternalMessage { get; private set; }

        public ExchangeRatesException(string? message, string externalMessage) : base(message)
        {
            this.ExternalMessage = externalMessage;
        }
    }
}
