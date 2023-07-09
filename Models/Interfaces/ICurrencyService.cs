using FluentResults;

namespace CurrencyConverter.Models.Interfaces
{
    public interface ICurrencyService
    {
        Task<Result<double>> ConvertCurrencyAsync(ConvertCurrencyRequest request);
    }
}
