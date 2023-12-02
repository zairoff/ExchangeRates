﻿namespace ExchangeRates.Domain.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }

        public string Base { get; set; }

        public IReadOnlyCollection<Dictionary<string, string>> Rates { get; set; }
    }
}
