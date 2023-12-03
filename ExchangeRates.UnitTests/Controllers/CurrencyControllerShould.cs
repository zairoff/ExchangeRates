using AutoFixture;
using ExchangeRates.Abstractions.Orchestration;
using ExchangeRates.Api.Controllers;
using ExchangeRates.Contracts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Net;

namespace ExchangeRates.UnitTests.Controllers
{
    public class CurrencyControllerShould
    {
        private readonly Fixture _fixture;
        private readonly ICurrencyOrchestration _currencyOrchestration;
        private readonly CurrencyController _currencyController;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyControllerShould()
        {
            _fixture = new Fixture();
            _currencyOrchestration = Substitute.For<ICurrencyOrchestration>();
            _logger = Substitute.For<ILogger<CurrencyController>>();
            _currencyController = new CurrencyController(_currencyOrchestration, _logger);
        }

        [Fact]
        public async Task GetLatest_ReturnsLatestRates()
        {
            var currencyRate = _fixture.Create<CurrencyRate>();

            _currencyOrchestration.GetLatestAsync().Returns(currencyRate);

            var result = (OkObjectResult)await _currencyController.GetLatest();

            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}
