using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeStats
{
    class NumberFormatter
    {
        public static string FormatNumber(int input)
        {
            if (input > 1e9)
            {
                return Math.Round((float)input / 1e9f, 1).ToString() + "B";
            }
            else if (input > 1e6)
            {
                return Math.Round((float)input / 1e6f, 1).ToString() + "M";
            }
            else if (input > 1e3)
            {
                return Math.Round((float)input / 1e3f, 1).ToString() + "k";
            }
            else
            {
                return input.ToString();
            }
        }
    }
}
