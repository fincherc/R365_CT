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

        //Function to add the numbers in the string
        //param - numbers: the string given in the console
        //int - return the sum of the numbers in the string
        public int Add(string numbers)
        {
            if (numbers == "")
            {
                return 0;
            }

            if (numbers.StartsWith("//"))
            {
                FindDelimiters(numbers);
            }
            
            string[] stringArr = Splitting(numbers);
            
            foreach (string number in stringArr)
            { 
                if(Int32.TryParse(number.Trim(), out int result))
                {
                    if (result < 0)
                        negativeNumbers.Add(result.ToString());
                    else if(result <= 1000)
                        mResult += result;
                }
            }
            
            if (negativeNumbers.Count > 0)
            {
                throw new Exception($"Negatives not allowed: '{string.Join(", ", negativeNumbers.ToArray())}'");
            }

            return mResult;
            
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
            //7.Delimiters can be of any length with the following format:  “//[delimiter]\n” for example: “//[***]\n1***2***3” should return 6 - done
            else
            {
                //delimiterList = delimiter.Split('[').Select(x => x.TrimEnd(']')).ToList();
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
                throw new Exception($"Negatives not allowed: '{string.Join(", ", negativeNumbers.ToArray())}'");
            }

            return mResult;
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
            return numbers.Split(new string[] { ",", "\\r", "\\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        //Function to remove the unqiue delimiters in the string after passing the check
        //param - numbers: the string given in the console
        //param - delimiter: the string encompassing the delimiters after passing the check
        //string[] - returns an array of the unique delimiters
        public string[] UniqueSplitting(string numbers, string delimiter)
        {
            return numbers.Split(Convert.ToChar(delimiter));
        }

        //Function to remove the longer, unique delimiters in the string after passing the check
        //param - numbers: the string given in the console
        //param - delimiter: the string encompassing the delimiters after passing the check
        //string[] - returns an array of the longer unique delimiters
        public string[] LongerUniqueSplitting(string numbers, string delimiter)
        {
            List<string> delimiterList = new List<string>();
            string newDelimiter = "";

            //8.Allow multiple delimiters like this:  “//[delim1][delim2]\n” for example “//[*][%]\n1*2%3” should return 6. - done
            //9.Make sure you can also handle multiple delimiters with length longer than one char - done
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
    }
}
