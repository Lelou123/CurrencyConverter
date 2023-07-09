using CurrencyConverter.Models;
using CurrencyConverter.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpPost]
        [Route("/ConvertCurrency")]
        public async Task<IActionResult> GetCurrencyConverted(ConvertCurrencyRequest request)
        {

            var result = await _currencyService.ConvertCurrencyAsync(request);


            if (result.IsFailed)
                return BadRequest(result.Errors.FirstOrDefault());



            return Ok(result.Value);
        }
    }
}
