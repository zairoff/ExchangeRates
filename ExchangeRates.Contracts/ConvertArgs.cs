namespace ExchangeRates.Contracts
{
    public class ConvertArgs
    {
        /// <summary>
        /// The value to be converted
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The base currency (3-letter code)
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// The target currency (3-letter code)
        /// </summary>
        public string To { get; set; }
    }
}
