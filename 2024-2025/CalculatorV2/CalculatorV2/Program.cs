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
            while (true)
            {
                Console.Write("Equation:");
                string input = Console.ReadLine().Replace(" ", ""); // Simply removes whitespaces

                string[] operators = { "+", "-", "*", "/" };

                List<char> usedOperators = Regex.Replace(input, "[a-zA-Z0-9()]", "").ToCharArray().ToList(); // Extracts all operators from string
                List<float> usedValues = input.Replace("(", "").Replace(")", "").Split(operators, StringSplitOptions.RemoveEmptyEntries).Select(float.Parse).ToList();

                if (input.Contains("(") || input.Contains(")"))
                {
                    List<List<int>> parenthesisLocations = new List<List<int>>(); // There could be a array here (I think)
                    List<string> usedValuesString = input.Split(operators, StringSplitOptions.RemoveEmptyEntries).ToList();
                    try
                    {
                        parenthesisLocations = ParenthesisMatcher(usedValuesString);
                        for (int i = 0; i < parenthesisLocations.Count(); i++)
                        {
                            try
                            {
                                List<float> currentValues = usedValues.GetRange(parenthesisLocations[i][0], Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]) + 1);
                                List<char> currentOperators = usedOperators.GetRange(parenthesisLocations[i][0], Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]));
                                usedValues[parenthesisLocations[i][0]] = Calculation(currentOperators, currentValues);
                            }
                            catch (ArgumentException)
                            {
                                List<float> currentValues = usedValues.GetRange(parenthesisLocations[i][0] - 1, Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]) + 1);
                                List<char> currentOperators = usedOperators.GetRange(parenthesisLocations[i][0] - 1, Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]));
                                usedValues[parenthesisLocations[i][0] - 1] = Calculation(currentOperators, currentValues);
                            }
                            usedValues.RemoveAt(i + 1);
                            usedOperators.RemoveAt(i);
                            DisplayEquation(usedValues, usedOperators);
                        }
                    }
                    catch (ArgumentOutOfRangeException) // This will thrown if there is a list with only one element 
                    {
                        Console.WriteLine("Missing parenthesis");
                    }
                }
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
            /*int highestPriority = 1000; // Set really high
            for (int i = 0; i < parenthesisLocations.Count(); i++)
            {
                if (Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]) < highestPriority)
                {
                    highestPriority = Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]);
                }
            }

            List<List<int>> temp = new List<List<int>>();

            for (int i = 0; i < parenthesisLocations.Count(); i++)
            {
                if (Math.Abs(parenthesisLocations[i][0] - parenthesisLocations[i][1]) == highestPriority)
                {
                    temp.Add(parenthesisLocations[i]);
                }
            }

            return temp;*/
            return parenthesisLocations;
        }
    }
}
