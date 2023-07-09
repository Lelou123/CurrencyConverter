
using CurrencyConverter.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CurrencyConverter.Models;

public class ConvertCurrencyRequest
{

    [Range(1, 4, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    [Required]
    public CurrencyEn FromCurrency { get; set; }

    [Range(1, 4, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    [Required]
    public CurrencyEn ToCurrency { get; set; }
    
    [Required]
    public double Value { get; set; }
}
