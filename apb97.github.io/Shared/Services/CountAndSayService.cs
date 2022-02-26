using System.Text;

namespace apb97.github.io.Shared.Services
{
    public class CountAndSayService
    {
        public CountAndSayService()
        {

        }

        public string CountAndSay(int numberOfSayings)
        {
            if (numberOfSayings < 0) return "OutOfRange";
            if (numberOfSayings == 1)
            {
                return "1";
            }
            return Count(CountAndSay(numberOfSayings - 1));
        }

        private string Count(string sentence)
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

            return builder.ToString();
        }
    }
}
