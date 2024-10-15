using System.Globalization;
using System.Text;

namespace apb97.github.io.Services;

public class SayService(APB97StringLocalizer<SayService> localizer)
{
    public async Task InitializeAsync(CultureInfo? cultureInfo)
    {
        await localizer.InitializeAsync(cultureInfo ?? CultureInfo.CurrentUICulture);
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