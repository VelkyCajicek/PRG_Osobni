using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char[,] player_array = new char[10, 10];
            char[,] enemy_array = new char[10, 10];
            for (int i = 0; i < player_array.GetLength(0); i++)
            {
                for (int j = 0; j < player_array.GetLength(1); j++)
                {
                    player_array[i, j] = 'W';
                    enemy_array[i, j] = 'W';
                }
            }
            DisplayArray(player_array);

            Console.ReadKey();
        }

        static void DisplayArray(char[,] ship_array)
        {
            char[] letter_array = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            Console.Write("  ");
            for (int i = 0; i < letter_array.Length; i++)
            {
                Console.Write($"{letter_array[i]} ");
            }
            Console.Write("\n");
            for (int i = 0; i < ship_array.GetLength(0); i++)
            {
                Console.Write($"{i + 1} ");
                for (int j = 0; j < ship_array.GetLength(1); j++)
                {
                    Console.Write($"{ship_array[i, j]} ");
                }
                Console.Write("\n");
            }
        }
        static char[,] Fill_Array_Randomly(char[,] ship_array)
        {
            Random rnd = new Random();
            int x_pos = rnd.Next(0, 10);
            int y_pos = rnd.Next(0, 10);
            List<int[]> directions = new List<int[]>()
            {
                new int[]{0,1},
                new int[]{1,0},
                new int[]{0,-1},
                new int[]{-1,0},
            };
            // Order of ships: AC (1x5), Battleship (1x4), Cruiser(1x3), Sub(1x3), Destroyer(1x2)
            Dictionary<char, int> ship_names = new Dictionary<char, int>()
            {
                {'A', 5 },
                {'B', 4 },
                {'C', 3 },
                {'S', 3 },
                {'D', 2 }
            };
            char[,] temp;
            for (int i = 0; i < ship_names.Count(); i++)
            {
                for (int j = 0; j < directions.Count(); j++)
                {
                    temp = ship_array;
                    
                }
            }
            


            return new char[,] {};
        }
    }
}
