using ExchangeRates.Abstractions.Services;
using ExchangeRates.Contracts;
using ExchangeRates.Domain;
using ExchangeRates.Services.Mappers;
using Microsoft.Extensions.Configuration;

namespace ExchangeRates.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly string _apiKey;

        public CurrencyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _url = configuration.GetSection("Services:FinancialService:Url").Value;
            _apiKey = configuration.GetSection("OpenExchangeRatesApi").Value;
        }

        public async Task<double> ConvertAsync(double value, string from, string to)
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_url}/convert/{value}/{from}/{to}?app_id={_apiKey}");
            using HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

            return await httpResponse.ProcessResponseAsync<double>();
        }

        public async Task<IReadOnlyCollection<Currency>> GetAsync()
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_url}/currencies.json?app_id={_apiKey}");
            using HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            var response = await httpResponse.ProcessResponseAsync<Dictionary<string, string>>();

            return CurrencyMapper.MapCurrencies(response);
        }

        public async Task<IReadOnlyCollection<Currency>> GetLatestAsync()
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_url}/latest.json?app_id={_apiKey}");
            using HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);
            var response = await httpResponse.ProcessResponseAsync<Dictionary<string, string>>();

            return CurrencyMapper.MapCurrencies(response);
        }
    }
}