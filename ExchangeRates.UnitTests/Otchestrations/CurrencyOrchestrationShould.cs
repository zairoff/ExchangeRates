using AutoFixture;
using ExchangeRates.Abstractions.Orchestration;
using ExchangeRates.Abstractions.Repositories;
using ExchangeRates.Abstractions.Services;
using ExchangeRates.Api.Orchestration;
using ExchangeRates.Contracts;
using ExchangeRates.Domain.Models;
using FluentAssertions;
using NSubstitute;
using System.Linq.Expressions;

namespace ExchangeRates.UnitTests.Otchestrations
{
    public class CurrencyOrchestrationShould
    {
        private readonly Fixture _fixture;
        private readonly IRepository<ExchangeRate> _repository;
        private readonly ICurrencyService _currencyService;
        private readonly ICurrencyOrchestration _currencyOrchestration;

        public CurrencyOrchestrationShould()
        {
            _fixture = new Fixture();
            _repository = Substitute.For<IRepository<ExchangeRate>>();
            _currencyService = Substitute.For<ICurrencyService>();

            _currencyOrchestration = new CurrencyOrchestration(_repository, _currencyService);
        }

        [Fact]
        public async Task GetLatestAsync_WhenExchangeRatesAvailableInRepo_ReturnsThem()
        {
            var exchangerate = _fixture.Create<ExchangeRate>();

            _repository.FindAsync(Arg.Any<Expression<Func<ExchangeRate, bool>>>())
                .Returns(exchangerate);

            CurrencyRate currencyRate = await _currencyOrchestration.GetLatestAsync();

            currencyRate.Should().NotBeNull();

            await _repository.Received(1).FindAsync(Arg.Any<Expression<Func<ExchangeRate, bool>>>());
            await _currencyService.DidNotReceive().GetLatestRateAsync();
            await _repository.DidNotReceive().AddAsync(Arg.Any<ExchangeRate>());
        }
    }
}
