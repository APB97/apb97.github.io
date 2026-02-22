using System.Text;
using apb97.github.io.Shared.Services.Localization;

namespace apb97.github.io.Services;

public class SayService(StringLocalizer<SayService> localizer)
{
    public async Task InitializeAsync(string? cultureName)
    {
        await localizer.InitializeAsync(cultureName);
    }

    public string Say(string digitString)
    {
        var builder = new StringBuilder();

        for (int i = 0; i < digitString.Length; i++)
        {
            if (i % 2 == 0)
            {
                builder.Append(localizer.Localize($"{digitString[i]} word"));
            }
            else
            {
                if (digitString[i - 1] == '1')
                {
                    builder.Append($" {localizer.Localize($"{digitString[i]} singular")}");
                }
                else
                {
                    builder.Append($" {localizer.Localize($"{digitString[i]} plural")}");
                }

                if (i != digitString.Length - 1)
                {
                    builder.Append(", ");
                }
            }
        }

        return builder.Append("<br/>").ToString();
    }
}