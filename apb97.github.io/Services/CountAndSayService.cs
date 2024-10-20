using System.Text;

namespace apb97.github.io.Services;

public class CountAndSayService(SayService? sayService)
{
    public async Task InitializeAsync(string? cultureName)
    {
        if (sayService is null) return;
        await sayService.InitializeAsync(cultureName);
    }

    public string CountAndSay(int numberOfSayings, StringBuilder details)
    {
        if (numberOfSayings <= 0 || numberOfSayings > 30) throw new ArgumentOutOfRangeException(nameof(numberOfSayings));
        if (numberOfSayings == 1)
        {
            return "1";
        }
        return Count(CountAndSay(numberOfSayings - 1, details), details);
    }

    private string Count(string sentence, StringBuilder details)
    {
        var digit = '0';
        var currentCounter = 0;
        var builder = new StringBuilder();

        for (int i = 0; i < sentence.Length; i++)
        {
            if (digit != sentence[i])
            {
                if (currentCounter != 0)
                {
                    builder.Append(currentCounter)
                        .Append(digit);
                }

                digit = sentence[i];
                currentCounter = 1;
            }
            else
            {
                currentCounter++;
            }
        }

        builder.Append(currentCounter)
            .Append(digit);

        var result = builder.ToString();
        details.Append(sayService?.Say(result));
        return result;
    }
}