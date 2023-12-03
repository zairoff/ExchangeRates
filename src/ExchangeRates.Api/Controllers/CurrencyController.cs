using ExchangeRates.Domain;
using ExchangeRates.Contracts;
using Microsoft.AspNetCore.Mvc;
using ExchangeRates.Abstractions.Orchestration;

namespace ExchangeRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyOrchestration _currencyOrchestration;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ICurrencyOrchestration currencyOrchestration, ILogger<CurrencyController> logger)
        {
            _currencyOrchestration = currencyOrchestration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var currencies = await _currencyOrchestration.GetAsync();

                return new OkObjectResult(currencies);
            }
            catch (ExchangeRatesException ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return new BadRequestObjectResult(ex.ExternalMessage);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpGet]
        [Route("Latest")]
        public async Task<IActionResult> GetLatest()
        {
            try
            {
                var rates = await _currencyOrchestration.GetLatestAsync();

                return new OkObjectResult(rates);
            }
            catch (ExchangeRatesException ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return new BadRequestObjectResult(ex.ExternalMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Convert")]
        public async Task<IActionResult> Convert([FromBody] ConvertArgs args)
        {
            try
            {
                var currencies = await _currencyOrchestration.ConvertAsync(args.Value, args.From, args.To);

                return new OkObjectResult(currencies);
            }
            catch (ExchangeRatesException ex)
            {
                _logger.LogWarning(ex, ex.Message);

                return new BadRequestObjectResult(ex.ExternalMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
