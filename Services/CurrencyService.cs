using CurrencyConverter.Models.Enums;
using CurrencyConverter.Models;
using CurrencyConverter.Models.Interfaces;
using Newtonsoft.Json;
using System.Globalization;
using FluentResults;
using static System.Runtime.InteropServices.JavaScript.JSType;
using RestSharp;

namespace CurrencyConverter.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly RestClient _restClient;
        
        public CurrencyService()
        {
            _restClient = new RestClient("https://www.alphavantage.co");
        }


        public async Task<Result<double>> ConvertCurrencyAsync(ConvertCurrencyRequest request)
        {

            CurrencyEn toCurrency = request.ToCurrency;
            CurrencyEn fromCurrency = request.FromCurrency;
            double amount = request.Value;

            var exchangeRateFromCurrency = await GetExchangeRateAsync(fromCurrency);
            var exchangeRateToCurrency = await GetExchangeRateAsync(toCurrency);

            var fromCurrencyRate = exchangeRateFromCurrency;
            var toCurrencyRate = exchangeRateToCurrency;

            var convertedAmount = amount * (fromCurrencyRate / toCurrencyRate);

            convertedAmount = Math.Round(convertedAmount * 100) / 100;

            return Result.Ok(convertedAmount);
        }



        private async Task<double> GetExchangeRateAsync(CurrencyEn currency)
        {
            var currencyRequest = new RestRequest("/query");
            currencyRequest.AddQueryParameter("function", "CURRENCY_EXCHANGE_RATE");
            currencyRequest.AddQueryParameter("from_currency", currency);
            currencyRequest.AddQueryParameter("to_currency", "USD");
            currencyRequest.AddQueryParameter("apikey", "your_Key");

            var currencyResponse = await _restClient.ExecuteAsync(currencyRequest);
            var currencyData = JsonConvert.DeserializeObject<AlphaVantageResponse>(currencyResponse.Content);

            var rate = double.Parse(currencyData.RealtimeCurrencyExchangeRate.ExchangeRate, CultureInfo.InvariantCulture);


            return rate;
        }

    }
}
