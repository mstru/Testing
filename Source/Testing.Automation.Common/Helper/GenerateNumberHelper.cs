using System;
using System.Text;

namespace Testing.Automation.Common.Helper
{
    public class GenerateNumberHelper
    {       
        public static string randomNumber(int length)
        {
            const string valid = "0123456789";                        
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                sb.Append(valid[rnd.Next(valid.Length)]);
            }

            return sb.ToString();
        }

        public static Double randomDouble(int decimalPoint, double start, double end)
        {
            Random rnd = new Random();
            var rNumber = rnd.NextDouble() * Math.Abs(end - start) + start;

            return Math.Round(rNumber, decimalPoint);                  
        }

        public static string random(int length)
        {
            const string valid = "12";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            for (int i = 1; i < length; i++)
            {
                sb.Append(valid[rnd.Next(valid.Length)]);
            }

            return sb.ToString();
        }
    }
}
