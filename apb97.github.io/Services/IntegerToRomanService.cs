using System.Text;

namespace apb97.github.io.Services;

public static class IntegerToRomanService
{
    private static readonly Dictionary<char, int> characterValues = new()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };
    private static readonly HashSet<char> _charsAllowedBeforeGreater = ['I', 'X', 'C'];

    public static string ToRoman(int number)
    {
        if (number <= 0 || number >= 4000)
            throw new ArgumentOutOfRangeException(nameof(number));

        var result = new StringBuilder();
        var characters = characterValues.Keys.ToArray();
        int currentAmount;
        foreach (var item in _charsAllowedBeforeGreater)
        {
            currentAmount = number / characterValues[item] % 10;
            number -= currentAmount * characterValues[item];
            if (currentAmount < 4)
            {
                result.Insert(0, new string(item, currentAmount));
            }
            else if (currentAmount == 4)
            {
                result.Insert(0, $"{item}{characters[Array.IndexOf(characters, item) + 1]}");
            }
            else if (currentAmount < 9)
            {
                result.Insert(0, $"{characters[Array.IndexOf(characters, item) + 1]}{new string(item, currentAmount - 5)}");
            }
            else if (currentAmount == 9)
            {
                result.Insert(0, $"{item}{characters[Array.IndexOf(characters, item) + 2]}");
            }
        }
        var thousandSymbol = characters.Last();
        currentAmount = number / characterValues[thousandSymbol];
        result.Insert(0, new string(thousandSymbol, currentAmount));
        return result.ToString();
    }
}