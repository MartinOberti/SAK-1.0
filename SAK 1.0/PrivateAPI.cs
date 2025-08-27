using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAK_1._0
{
    class PrivateAPI
    {
        public int randomNumber(int number1, int number2)
        {
            Random rnd = new Random();
            int answer = rnd.Next(number1, number2+1);
            return answer;
        }
    }
}
