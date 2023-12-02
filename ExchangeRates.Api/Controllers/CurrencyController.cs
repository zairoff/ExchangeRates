using ExchangeRates.Abstractions.Services;
using ExchangeRates.Domain;
using ExchangeRates.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _financialService;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ICurrencyService financialService, ILogger<CurrencyController> logger)
        {
            _financialService = financialService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var currencies = await _financialService.GetAsync();

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
                var currencies = await _financialService.GetLatestAsync();

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

        [HttpPost]
        [Route("Convert")]
        public async Task<IActionResult> Convert([FromBody] ConvertArgs args)
        {
            try
            {
                var currencies = await _financialService.ConvertAsync(args.Value, args.From, args.To);

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
