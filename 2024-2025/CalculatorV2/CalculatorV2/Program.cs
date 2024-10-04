using System;
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
            string[] operators = { "+", "-", "*", "/" };
            while (true)
            {
                Console.Write("Equation:");
                string input = Console.ReadLine().Replace(" ", ""); // Simply removes whitespaces

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
                    int parenthesisLength = 5000;
                    int parenthesisIndex = 0;
                    for (int i = 0; i < parenthesisLocations.Count(); i++)
                    {
                        if (Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]) < parenthesisLength)
                        {
                            parenthesisIndex = i;
                            parenthesisLength = Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]);
                        }
                    }

                    //DisplayList(parenthesisLocations[parenthesisIndex]);
                    //Console.WriteLine(parenthesisLength);

                    List<float> currentValues = usedValues.GetRange(parenthesisLocations[parenthesisIndex][0], Math.Abs(parenthesisLocations[parenthesisIndex][0] - parenthesisLocations[parenthesisIndex][1]) + 1);
                    List<char> currentOperators = usedOperators.GetRange(parenthesisLocations[parenthesisIndex][0], Math.Abs(parenthesisLocations[parenthesisIndex][0] - parenthesisLocations[parenthesisIndex][1]));
                    string sub = input.Substring(parenthesisLocations[parenthesisIndex][0] * 2, 3 + 2 * parenthesisLength);
                    //Console.WriteLine(sub);
                    if (parenthesisLocations[parenthesisIndex][0] == 0)
                    {
                        input = input.Replace(input.Substring(parenthesisLocations[parenthesisIndex][0] * 2, 3 + 2 * parenthesisLength), Calculation(currentOperators, currentValues).ToString());
                    }
                    else
                    {
                        try
                        {
                            //Console.WriteLine(input);
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
                usedOperators = Regex.Replace(input, "[a-zA-Z0-9()]", "").ToCharArray().ToList(); // Extracts all operators from string
                usedValues = input.Replace("(", "").Replace(")", "").Split(operators, StringSplitOptions.RemoveEmptyEntries).Select(float.Parse).ToList();
                float result = Calculation(usedOperators, usedValues);
                Console.WriteLine(result);
            }
            
            Console.ReadKey();
        }

        static void DisplayList(dynamic array)
        {
            foreach (var item in array)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

        static void DisplayEquation(List<float> values, List<char> operators)
        {
            for (int i = 0; i < operators.Count(); i++)
            {
                Console.Write($"{values[i]} {operators[i]} ");
            }
            Console.Write(values.Last());
            Console.WriteLine();
        }

        static float Calculation(List<char> usedOperators, List<float> usedValues)
        {
            while (usedOperators.Contains('*')|| usedOperators.Contains('/')) // Ensures that BIDMAS rules are followed
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
