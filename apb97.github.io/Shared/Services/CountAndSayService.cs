using System.Text;

namespace apb97.github.io.Shared.Services
{
    public class CountAndSayService
    {
        private readonly SayService? _sayService;

        public CountAndSayService(SayService? sayService)
        {
            _sayService = sayService;
        }

        public string CountAndSay(int numberOfSayings, StringBuilder details)
        {
            if (numberOfSayings <= 0 || numberOfSayings > 30) return "OutOfRange";
            if (numberOfSayings == 1)
            {
                return "1";
            }
            return Count(CountAndSay(numberOfSayings - 1, details), details);
        }

        private string Count(string sentence, StringBuilder details)
        {
            char digit = '0';
            int currentCounter = 0;
            StringBuilder builder = new StringBuilder();

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

            string result = builder.ToString();
            details.Append(_sayService?.Say(result));
            return result;
        }
    }
}
