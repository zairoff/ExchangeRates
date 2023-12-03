using AutoFixture;
using ExchangeRates.Services.Mappers;
using FluentAssertions;

namespace ExchangeRates.UnitTests.Mappers
{
    public class CurrencyMapperShould
    {
        private readonly Fixture _fixture;

        public CurrencyMapperShould()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void MapCurrencyRates()
        {
            var incomingRates = _fixture.Create<Dictionary<string, string>>();

            var result = CurrencyMapper.MapCurrencyRates(incomingRates);

            result.Should().NotBeNull();
            result.Rates.Count.Should().Be(incomingRates.Count);
        }
    }
}
