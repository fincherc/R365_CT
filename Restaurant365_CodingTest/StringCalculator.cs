using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Restaurant365_CodingTest
{
    class StringCalculator
    {
        private int mResult = 0;
        private string mDelimiter;
        List<string> negativeNumbers = new List<string>();
        private bool mCheckFormat;
        private bool mUniqueStart = false;

        //Function to add the numbers in the string
        //param - numbers: the string given in the console
        //int - return the sum of the numbers in the string
        public int Add(string numbers)
        {
            if (numbers == "")
            {
                return 0;
            }

            mCheckFormat = CheckFormat(numbers);

            if (mCheckFormat)
            {
                if (!mUniqueStart)
                {
                    string[] stringArr = Splitting(numbers);

                    foreach (string number in stringArr)
                    {
                        if (Int32.TryParse(number.Trim(), out int result))
                        {
                            if (result < 0)
                                negativeNumbers.Add(result.ToString());
                            else if (result <= 1000)
                                mResult += result;
                        }
                    }

                    if (negativeNumbers.Count > 0)
                    {
                        NegativeNumbersException(negativeNumbers);
                    }
                }
                else
                {
                    FindDelimiters(numbers);
                }

                return mResult;
            }
            else
            {
                return mResult;
            }

        }

        //Function to add the numbers in the string with a unique delimiter string
        //param - numbers: the string given in the console
        //param - delimiter: the unique character string pulled from the numbers string
        //int - return the sum of the numbers in the string
        public int Add(String numbers, String delimiter)
        {
            string[] stringArr = new string[] { };
            
            if (delimiter.Length > 0 && delimiter.Length < 2)
            {
                stringArr = UniqueSplitting(numbers, delimiter);
            }
            else
            {
                stringArr = LongerUniqueSplitting(numbers, delimiter);
            }
            
            foreach(string number in stringArr)
            {
                if (Int32.TryParse(number.Trim(), out int result))
                {
                    if (result < 0)
                        negativeNumbers.Add(result.ToString());
                    else if (result <= 1000)
                        mResult += result;
                }
            }

            if (negativeNumbers.Count > 0)
            {
                NegativeNumbersException(negativeNumbers);
            }

            return mResult;
        }

        //Function to check the format of the string to check on whether it is in proper context
        //param - input: the string given in the console
        //bool - return true if format is correct, false otherwise with a statement
        public bool CheckFormat(string input)
        {
            if (input.Contains(",") || input.Contains("\\n"))
            {
                mCheckFormat = true;
                return mCheckFormat;
            }
            else if (input.StartsWith("//"))
            {
                mCheckFormat = true;
                mUniqueStart = true;
                return mCheckFormat;
            }
            else
            {
                Console.WriteLine("User input incorrect, the input must start with '//' or use a comma ',' and/or use a new line. Your answer will come out as 0.");
                return mCheckFormat;
            }
        }

        //Function to remove the noted delimiters in the string after passing the check and enter the new Add function
        //param - numbers: the string given in the console
        //void - don't need to return anything
        public void FindDelimiters(string numbers)
        {
            int delimiterIndex = numbers.IndexOf("//") + 2;
            mDelimiter = numbers.Substring(delimiterIndex, numbers.IndexOf("\\n") - delimiterIndex);
            string pastDelimiter = numbers.Substring(numbers.IndexOf("n") + 1);

            Add(pastDelimiter, mDelimiter);
        }

        //Function to split the delimiters
        //param - numbers: the string given in the console
        //string[] - returns an array of the delimiters
        public string[] Splitting(string numbers)
        {
                return numbers.Split(new string[] { ",", "\\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        //Function to remove the unqiue delimiters in the string after passing the check
        //param - numbers: the string given in the console
        //param - delimiter: the string encompassing the unique delimiters after passing the check
        //string[] - returns an array of the unique delimiters
        public string[] UniqueSplitting(string numbers, string delimiter)
        {
            return numbers.Split(Convert.ToChar(delimiter));
        }

        //Function to remove the longer, unique delimiters in the string after passing the check
        //param - numbers: the string given in the console
        //param - delimiter: the string encompassing the longer, unique delimiters after passing the check
        //string[] - returns an array of the longer unique delimiters
        public string[] LongerUniqueSplitting(string numbers, string delimiter)
        {
            List<string> delimiterList = new List<string>();
            string newDelimiter = "";

            if (delimiter.StartsWith("[") && delimiter.EndsWith("]"))
            {

                char[] delimiterChar = delimiter.ToCharArray();

                for (int i = 0; i < delimiterChar.Length; i++)
                {
                    if (delimiterChar[i] != '[' && delimiterChar[i] != ']')
                    {
                        if (delimiterChar[i + 1] != ']')
                            newDelimiter += delimiterChar[i].ToString();
                        else
                            delimiterList.Add(delimiterChar[i].ToString());
                    }

                    if (delimiterChar[i] == ']')
                    {
                        newDelimiter = "";
                    }
                }
            }

            return numbers.Split(delimiterList.ToArray(), StringSplitOptions.None);
        }

        //Function to execute if there are negative numbers
        //param - Numbers: List of numbers that are negative
        //Exception - displays an exception message noting the numbers that are negative from the string
        public void NegativeNumbersException(List<string> Numbers)
        {
            throw new Exception($"Negatives not allowed: '{string.Join(", ", Numbers.ToArray())}'");
        }
    }
}
