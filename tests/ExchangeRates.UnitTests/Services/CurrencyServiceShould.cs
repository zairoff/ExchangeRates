using AutoFixture;
using ExchangeRates.Abstractions.Services;
using ExchangeRates.Domain;
using ExchangeRates.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NSubstitute;
using RichardSzalay.MockHttp;
using System.Net;

namespace ExchangeRates.UnitTests.Services
{
    public class CurrencyServiceShould
    {
        private readonly ICurrencyService _currencyService;
        private readonly MockHttpMessageHandler _handler;
        private readonly Fixture _fixture;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey = "aaa";
        private readonly string _url = "https://test.com";

        public CurrencyServiceShould()
        {
            _fixture = new Fixture();

            _handler = new MockHttpMessageHandler();
            _configuration = Substitute.For<IConfiguration>();
            _configuration.GetSection("Services:FinancialService:Url").Value.Returns(_url);
            _configuration.GetSection("OpenExchangeRatesApi").Value.Returns(_apiKey);

            var client = _handler.ToHttpClient();
            _currencyService = new CurrencyService(client, _configuration);

        }

        [Fact]
        public async Task GetAsync_Returns_Currencies()
        {
            var expectedResponse = _fixture.Create<Dictionary<string, string>>();

            _handler.When(HttpMethod.Get, $"{_url}/currencies.json?app_id={_apiKey}")
                    .Respond("application/json", JsonConvert.SerializeObject(expectedResponse));

            var currencies = await _currencyService.GetAsync();

            currencies.Should().NotBeNullOrEmpty();
            currencies.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAsync_Throws_WhenHttpRequestIsFailed()
        {
            var expectedResponse = _fixture.Create<Dictionary<string, string>>();

            _handler.When(HttpMethod.Get, $"{_url}/currencies.json?app_id={_apiKey}")
                    .Respond(HttpStatusCode.BadRequest);

            await _currencyService.Awaiting(x => x.GetAsync())
                .Should()
                .ThrowAsync<ExchangeRatesException>();
        }
    }
}
