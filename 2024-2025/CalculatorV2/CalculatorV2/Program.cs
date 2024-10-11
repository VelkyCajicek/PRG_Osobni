using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculatorV2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] operators = { "+", "-", "*", "/", "^" };
            string[] functions = { "sin", "cos", "tg", "cotg" };
            Dictionary<string, int> numberBases = new Dictionary<string, int>() // Converting between bases
            {
                { "binary", 2 },
                { "octal", 8 },
                { "decimal", 10 },
                { "hexadecimal", 16 }
            };
            // Additional features
            float ans = 0; // Reusing the value from previous calculation
            int nBIndex = 10;
            string nBName = "decimal";
            Help();
            while (true)
            {
                Console.Write($"({nBName}) Equation:");
                string input = Console.ReadLine().Replace(" ", "").ToLower().Replace("ans", ans.ToString()); // Simply removes whitespaces

                foreach (KeyValuePair<string, int> nB in numberBases) // Changes number base
                {
                    if(nB.Key == input)
                    {
                        nBIndex = nB.Value;
                        nBName = nB.Key;
                        Console.Write($"({nBName}) Equation:");
                        input = Console.ReadLine().Replace(" ", "").ToLower().Replace("ans", ans.ToString());
                    }
                }

                List<char> usedOperators = new List<char>();
                List<float> usedValues = new List<float>();

                while (input.Contains('(') || input.Contains(')'))
                {
                    usedOperators = Regex.Replace(input, "[a-zA-Z0-9()]", "").ToCharArray().ToList(); // Extracts all operators from string
                    usedValues = input.Replace("(", "").Replace(")", "").Split(operators, StringSplitOptions.RemoveEmptyEntries).Select(float.Parse).ToList();

                    List<string> usedValuesString = input.Split(operators, StringSplitOptions.RemoveEmptyEntries).ToList();
                    List<List<int>> parenthesisLocations = new List<List<int>>(); // There could be a array here (I think)

                    parenthesisLocations = ParenthesisMatcher(usedValuesString);

                    // Find smallest equation in parenthesis list
                    int parenthesisLength = 5000; // Program tries to find lowest value so initial value is just set to a random high value
                    int parenthesisIndex = 0;
                    try
                    {
                        for (int i = 0; i < parenthesisLocations.Count(); i++)
                        {
                            if (Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]) < parenthesisLength)
                            {
                                parenthesisIndex = i;
                                parenthesisLength = Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        input = input.Replace("(", "").Replace(")", "");
                        break;
                    }

                    List<float> currentValues = usedValues.GetRange(parenthesisLocations[parenthesisIndex][0], Math.Abs(parenthesisLocations[parenthesisIndex][0] - parenthesisLocations[parenthesisIndex][1]) + 1);
                    List<char> currentOperators = usedOperators.GetRange(parenthesisLocations[parenthesisIndex][0], Math.Abs(parenthesisLocations[parenthesisIndex][0] - parenthesisLocations[parenthesisIndex][1]));
                    string sub = input.Substring(parenthesisLocations[parenthesisIndex][0] * 2, 3 + 2 * parenthesisLength);

                    if (parenthesisLocations[parenthesisIndex][0] == 0)
                    {
                        input = input.Replace(input.Substring(parenthesisLocations[parenthesisIndex][0] * 2, 3 + 2 * parenthesisLength), Calculation(currentOperators, currentValues).ToString());
                    }
                    else
                    {
                        try
                        {
                            input = input.Replace(input.Substring(parenthesisLocations[parenthesisIndex][0] * 2 + 1, 3 + 2 * parenthesisLength), Calculation(currentOperators, currentValues).ToString());
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            input = input.Replace(input.Substring(parenthesisLocations[parenthesisIndex][0] * 2 + 1, 3 + 2 * parenthesisLength - 1), Calculation(currentOperators, currentValues).ToString());
                            input = input.Replace("(", "").Replace(")", "");
                            break;
                        }
                    }
                    Console.WriteLine(input);
                }
                try // Throws error if input is not in correct format
                {
                    usedOperators = Regex.Replace(input, "[a-zA-Z0-9()]", "").ToCharArray().ToList(); // Extracts all operators from string
                    usedValues = input.Replace("(", "").Replace(")", "").Split(operators, StringSplitOptions.RemoveEmptyEntries).Select(float.Parse).ToList();
                    float result = Calculation(usedOperators, usedValues);
                    ans = result; // Adds value to ans variable
                    if(nBIndex != 10) // Doesnt work yet
                    {
                        Console.WriteLine(Convert.ToString(Convert.ToInt32(result), nBIndex));
                    }
                    else
                    {
                        Console.WriteLine(result.ToString());
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Input is not in correct format");
                }
            }
            Console.ReadKey();
        }

        static void Help() 
        {
            Console.WriteLine(" - Allowed operations : +, -, *, ^");
            Console.WriteLine(" - Calculator follows BIDMAS rules of order and allows for parenthesis");
            Console.WriteLine(" - In order to change number base just write the name of it (e.g. binary, octal, decimal, hexadecimal)");
            Console.WriteLine(" - Right now it just writes the result in the given number base, so user input should be decimal");
            Console.WriteLine();
        }

        static void DisplayList(dynamic array) // To visualize states of lists easier
        {
            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        static void DisplayEquation(List<float> values, List<char> operators) // To visualize the state of the equation
        {
            for (int i = 0; i < operators.Count(); i++)
            {
                Console.Write($"{values[i]} {operators[i]} ");
            }
            Console.Write(values.Last());
            Console.WriteLine();
        }

        static float Calculation(List<char> usedOperators, List<float> usedValues) // Calculates the equation without parenthasis
        {
            while (usedOperators.Contains('^')) // Ensures that BIDMAS rules are followed
            {
                int index = usedOperators.IndexOf('^');
                usedValues[index] = (float)Math.Pow(usedValues[index], usedValues[index + 1]);
                usedValues.RemoveAt(index + 1);
                usedOperators.RemoveAt(index);
            }
            
            while (usedOperators.Contains('*') || usedOperators.Contains('/')) 
            {
                if (usedOperators.IndexOf('*') != -1)
                { // These blocks of code find the first index of operator and then removes both values and operator (which were used) from list
                    int index = usedOperators.IndexOf('*');
                    usedValues[index] = usedValues[index] * usedValues[index + 1];
                    usedValues.RemoveAt(index + 1);
                    usedOperators.RemoveAt(index);
                }
                if (usedOperators.IndexOf('/') != -1)
                {
                    int index = usedOperators.IndexOf('/');
                    usedValues[index] = usedValues[index] / usedValues[index + 1];
                    usedValues.RemoveAt(index + 1);
                    usedOperators.RemoveAt(index);
                }
            }

            while (usedValues.Count() != 1)
            {
                if (usedOperators.IndexOf('+') != -1)
                {
                    int index = usedOperators.IndexOf('+');
                    usedValues[index] = usedValues[index] + usedValues[index + 1];
                    usedValues.RemoveAt(index + 1);
                    usedOperators.RemoveAt(index);
                }
                if (usedOperators.IndexOf('-') != -1)
                {
                    int index = usedOperators.IndexOf('-');
                    usedValues[index] = usedValues[index] - usedValues[index + 1];
                    usedValues.RemoveAt(index + 1);
                    usedOperators.RemoveAt(index);
                }
            }
            return usedValues[0];
        }
        static List<List<int>> ParenthesisMatcher(List<string> usedValues)
        {
            List<List<int>> parenthesisLocations = new List<List<int>>(); // There could be a array here (I think)
            List<float> test = new List<float>();
            int index = 0; // Index to determine which is the next list that requires a closed parenthesis
            for (int i = 0; i < usedValues.Count(); i++)
            {
                if (usedValues[i].Contains("(")) // Creates new list for every open parenthesis
                {
                    for (int j = 0; j < usedValues[i].Count(f => f == '('); j++) // For every occurence of the open parenthesis
                    {
                        parenthesisLocations.Add(new List<int>() { i });
                    }
                }
                if (usedValues[i].Contains(")")) 
                {
                    for (int j = 0; j < usedValues[i].Count(f => f == ')'); j++)
                    {
                        if (parenthesisLocations.Count() == index + 1) // If there is only one possible list to append to
                        {
                            parenthesisLocations[index].Insert(1, i);
                            index++;
                        }
                        else
                        {
                            int moveValue = 1; // Value that goes up with every iteration
                            while (true) // Checks the lists in reverse order and once it finds an list with 1 element it appends the index
                            {
                                if (parenthesisLocations[parenthesisLocations.Count() - moveValue].Count() == 2)
                                {
                                    moveValue++;
                                }
                                else
                                {
                                    parenthesisLocations[parenthesisLocations.Count() - moveValue].Insert(1, i);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return parenthesisLocations;
        }
    }
}
