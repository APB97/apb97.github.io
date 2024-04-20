using Microsoft.Extensions.Localization;
using System.Text;

namespace apb97.github.io.Services;

public class IntegerToRomanService
{
    private readonly Dictionary<char, int> _charValues;
    private readonly HashSet<char> _charsAllowedBeforeGreater;
    private readonly IStringLocalizer<IntegerToRomanService> _localizer;

    public IntegerToRomanService(IStringLocalizer<IntegerToRomanService> localizer)
    {
        _localizer = localizer;
        _charValues = new Dictionary<char, int>()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };
        _charsAllowedBeforeGreater = new HashSet<char>() { 'I', 'X', 'C' };
    }

    public string ToRoman(int number)
    {
        if (number <= 0 || number >= 4000)
            return _localizer["OutOfRange"];

        StringBuilder result = new();
        var keys = _charValues.Keys.ToArray();
        int currentAmount;
        foreach (var item in _charsAllowedBeforeGreater)
        {
            currentAmount = number / _charValues[item] % 10;
            number -= currentAmount * _charValues[item];
            if (currentAmount < 4)
            {
                result.Insert(0, new string(item, currentAmount));
            }
            else if (currentAmount == 4)
            {
                result.Insert(0, $"{item}{keys[Array.IndexOf(keys, item) + 1]}");
            }
            else if (currentAmount < 9)
            {
                result.Insert(0, $"{keys[Array.IndexOf(keys, item) + 1]}{new string(item, currentAmount - 5)}");
            }
            else if (currentAmount == 9)
            {
                result.Insert(0, $"{item}{keys[Array.IndexOf(keys, item) + 2]}");
            }
        }
        var thousandSymbol = keys.Last();
        currentAmount = number / _charValues[thousandSymbol];
        result.Insert(0, new string(thousandSymbol, currentAmount));
        return result.ToString();
    }
}