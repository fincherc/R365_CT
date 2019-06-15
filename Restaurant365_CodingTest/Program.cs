using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant365_CodingTest
{
    class Program
    {

        static void Main(string[] args)
        {
            StringCalculator calc = new StringCalculator();

            Console.Write("Please enter a string for the String Calculator: ");
            string mReadIn = Console.ReadLine();

            Console.Write(calc.Add(mReadIn) + "\n");
            Console.ReadKey();
        }
    }
}
