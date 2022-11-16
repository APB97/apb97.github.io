using Microsoft.Extensions.Localization;
using System.Text;

namespace apb97.github.io.Shared.Services;

public class SayService
{
    private readonly IStringLocalizer<SayService> _localizer;

    public SayService(IStringLocalizer<SayService> localizer)
    {
        _localizer = localizer;
    }

    public string Say(string digitString)
    {
        var builder = new StringBuilder();

        for (int i = 0; i < digitString.Length; i++)
        {
            if (i % 2 == 0)
            {
                builder.Append(_localizer[$"{digitString[i]} word"]);
            }
            else
            {
                if (digitString[i - 1] == '1')
                {
                    builder.Append($" {_localizer[$"{digitString[i]} singular"]}");
                }
                else
                {
                    builder.Append($" {_localizer[$"{digitString[i]} plural"]}");
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