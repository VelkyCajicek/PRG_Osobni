using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            char[,] player_array = new char[10, 10];
            char[,] enemy_array = new char[10, 10];
            char[,] known_enemy_array = new char[10, 10];
            char[] letter_array = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            Dictionary<char, int> ship_names = new Dictionary<char, int>()
            {
                {'A', 5 },
                {'B', 4 },
                {'C', 3 },
                {'S', 3 },
                {'D', 2 }
            };

            int x_attack;
            int y_attack;
            int blast_size_x;
            int blast_size_y;
            string user_input;
            bool auto_play = false;
            bool cheats = false;
            List<string> game_transcript = new List<string>();

            for (int i = 0; i < player_array.GetLength(0); i++)
            {
                for (int j = 0; j < player_array.GetLength(1); j++)
                {
                    player_array[i, j] = 'W';
                    enemy_array[i, j] = 'W';
                    known_enemy_array[i, j] = 'U';
                }
            }
            enemy_array = Fill_Array_Randomly(enemy_array, ship_names);
            // Ask user whether he wants to have automatic ship placement
            if (Verify_User_Input(question: "Automatically generate your ship placement (y/n):") == "y") player_array = Fill_Array_Randomly(player_array, ship_names);
            else player_array = Manually_Fill_Array(player_array, letter_array, ship_names);
            // Ask user whether he wants the game to run automatically
            if (Verify_User_Input(question: "Automatically play out this game (y/n):") == "y") auto_play = true;
            if (Verify_User_Input(question: "Activate cheats? (Different bomb sizes)") == "y") cheats = true;
            // Main Game
            while (true)
            {
                Console.Clear();
                Display_Playing_Field(player_array, known_enemy_array, letter_array);
                if (Check_Game_State(player_array)) { Console.WriteLine("Enemy wins"); break; }
                if (Check_Game_State(enemy_array)) { Console.WriteLine("Player wins"); break; }

                // Player attack   
                if (auto_play)
                {
                    while (true)
                    {
                        x_attack = rnd.Next(0, 10);
                        y_attack = rnd.Next(0, 10);
                        if (enemy_array[y_attack, x_attack] != 'X') break;
                    }
                    Thread.Sleep(100);
                    game_transcript.Add($"{letter_array[y_attack]}{x_attack}");
                }
                else
                {
                    Console.WriteLine("Where would you like to attack? (e.g C3)");
                    if (cheats)
                    {
                        Console.WriteLine("Since cheats are active you may choose the size of your bomb");
                        Console.WriteLine("Format should be e.g. C3/3x3" );
                    }
                    while (true)
                    {
                        user_input = Console.ReadLine().ToUpper();
                        if (Regex.IsMatch(user_input, "^[A-Za-z][0-9]+$"))
                        {
                            if (letter_array.Contains(user_input[0]))
                            {
                                x_attack = Array.IndexOf(letter_array, user_input[0]);
                                y_attack = int.Parse(user_input.Substring(1, user_input.Length - 1)) - 1;
                                if (known_enemy_array[y_attack, x_attack] == 'U') break;
                                else Console.WriteLine("Already shot here");
                            }
                            else Console.WriteLine($"{user_input} is not a valid coordinate");
                        }
                        else if(Regex.IsMatch(user_input, "^[A-Za-z][0-9]+/[0-9]+x[0-9]+$") && cheats)
                        {
                            if (letter_array.Contains(user_input[0]))
                            {
                                x_attack = Array.IndexOf(letter_array, user_input[0]);
                                y_attack = int.Parse(user_input.Substring(1, user_input.Length - 1)) - 1;
                                string[] temp = user_input.Split('/');
                                blast_size_x = int.Parse(temp[0]);
                                blast_size_y = int.Parse(temp[2]);
                            }
                            else Console.WriteLine($"{user_input} is not a valid coordinate");
                        }
                        else Console.WriteLine("Input in incorrect format");
                    }
                }
                known_enemy_array[y_attack, x_attack] = enemy_array[y_attack, x_attack];
                enemy_array[y_attack, x_attack] = 'X';
                // Enemy attack
                while (true)
                {
                    x_attack = rnd.Next(0, 10);
                    y_attack = rnd.Next(0, 10);
                    if (player_array[y_attack, x_attack] != 'X') break;
                }
                player_array[x_attack, y_attack] = 'X';
            }
            // Write game transcript

            if(Verify_User_Input(question:"Would you like to print out the game transcript? (y/n)") == "y")
            {
                for (int i = 0; i < game_transcript.Count(); i++)
                {
                    if (i % 2 == 0) Console.WriteLine($"{i}. Player attacked {game_transcript[i]}");
                    else Console.WriteLine($"{i}. Enemy attacked {game_transcript[i]}");
                }
            }
            Console.ReadKey();
        }
        static bool Check_Game_State(char[,] ship_array)
        {
            char[] ship_names = new char[] {'A', 'B', 'C', 'D', 'S' };
            for (int i = 0; i < ship_array.GetLength(0); i++)
            {
                for (int j = 0; j < ship_array.GetLength(1); j++)
                {
                    if (Array.IndexOf(ship_names, ship_array[i, j]) != -1) return false;
                }
            }
            return true;
        }
        static string Verify_User_Input(string question)
        {
            string user_input;
            Console.Write($"{question} ");
            while (true)
            {
                user_input = Console.ReadLine().ToLower();
                if (user_input == "y" || user_input == "n") break;
                else Console.WriteLine("Invalid input"); ;
            }

            return user_input;
        }
        static void DisplayArray(char[,] ship_array, char[] letter_array)
        {
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
                    switch (ship_array[i,j])
                    {
                        case 'W': Console.ForegroundColor = ConsoleColor.Blue; break;
                        case 'X': Console.ForegroundColor = ConsoleColor.Red; break;
                        case 'U': Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                        default: Console.ForegroundColor = ConsoleColor.Yellow; break;
                    }
                    Console.Write($"{ship_array[i, j]} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n");
            }
        }
        static void Display_Playing_Field(char[,] player_ship_array, char[,] enemy_ship_array, char[] letter_array)
        {
            DisplayArray(enemy_ship_array, letter_array);
            Console.WriteLine();
            for (int i = 0; i < player_ship_array.GetLength(0); i++)
            {
                Console.Write("##");
            }
            Console.WriteLine();
            Console.WriteLine();
            DisplayArray(player_ship_array, letter_array);
        }
        static bool Array_Check(char[,] ship_array, int x_pos, int y_pos, int direction, int ship_length)
        {
            if (direction == 0)
            {
                if (x_pos + ship_length > ship_array.GetLength(0)) return false;
                else
                {
                    for (int i = 0; i < ship_length; i++)
                    {
                        if (ship_array[x_pos + i, y_pos] != 'W') return false;
                    }
                    return true;
                }
            }
            else
            {
                if (y_pos + ship_length > ship_array.GetLength(1)) return false;
                else
                {
                    for (int i = 0; i < ship_length; i++)
                    {
                        if (ship_array[x_pos, y_pos + 1] != 'W') return false;
                    }
                    return true;
                }
            }
        }
        static char[,] Fill_Array_Randomly(char[,] ship_array, Dictionary<char, int> ship_names)
        {
            Random rnd = new Random();
            int x_pos;
            int y_pos;
            int direction;
            // Order of ships: AC (1x5), Battleship (1x4), Cruiser(1x3), Sub(1x3), Destroyer(1x2)

            for (int i = 0; i < ship_names.Count(); i++)
            {
                while (true)
                {
                    x_pos = rnd.Next(0, 9);
                    y_pos = rnd.Next(0, 9);
                    direction = rnd.Next(0, 2);
                    if(Array_Check(ship_array, x_pos, y_pos, direction, ship_names.Values.ElementAt(i)))
                    {
                        if (direction == 0)
                        {
                            for (int j = 0; j < ship_names.Values.ElementAt(i); j++)
                            {
                                ship_array[x_pos + j, y_pos] = ship_names.Keys.ElementAt(i);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < ship_names.Values.ElementAt(i); j++)
                            {
                                ship_array[x_pos, y_pos + j] = ship_names.Keys.ElementAt(i);
                            }
                        }
                        break;
                    }
                }
            }

            return ship_array;
        }
        static char[,] Manually_Fill_Array(char[,] ship_array, char[] letter_array, Dictionary<char, int> ship_names)
        {
            string user_input;
            int x_pos;
            int y_pos;
            int direction = 1;
            string[] ship_names_array = { "Aircraft carrier", "Battleship", "Cruiser", "Submarine", "Destroyer" };

            Console.WriteLine("You will first select a start point and then whether the given ship will be place horizontally or vertically");
            Console.WriteLine("The ships will be placed in the following order: ");
            Console.WriteLine("AC (1x5), Battleship (1x4), Cruiser(1x3), Sub(1x3), Destroyer(1x2)");
            Console.WriteLine("Could you write the start coordinate? e.g. C3");
            
            DisplayArray(ship_array, letter_array);
            
            for (int i = 0; i < ship_names.Count(); i++)
            {
                while (true)
                {
                    while (true) // Verify coordinate
                    {
                        Console.WriteLine($"You are currently choosing the position of {ship_names_array[i]}");
                        user_input = Console.ReadLine().ToUpper();
                        if (Regex.IsMatch(user_input, "[A-Za-z][0-9]+"))
                        {
                            if (letter_array.Contains(user_input[0]))
                            {
                                x_pos = Array.IndexOf(letter_array, user_input[0]);
                                y_pos = int.Parse(user_input.Substring(1, user_input.Length - 1)) - 1;
                                if (ship_array[y_pos, x_pos] == 'W') break;
                                else Console.WriteLine("Already a ship here");
                            }
                            else Console.WriteLine($"{user_input} is not a valid coordinate");
                        }
                        else Console.WriteLine("Input in incorrect format");
                    }
                    Console.WriteLine("Now the ships orientation");
                    while (true)
                    {
                        user_input = Console.ReadLine().ToLower();
                        if (user_input == "h" || user_input == "v") break;
                        else Console.WriteLine("Invalid input");
                    }
                    if (user_input == "h") direction = 0;
                    if (Array_Check(ship_array, x_pos, y_pos, direction, ship_names.Keys.ElementAt(i)))
                    {
                        if (direction == 0)
                        {
                            for (int j = 0; j < ship_names.Values.ElementAt(i); j++)
                            {
                                ship_array[x_pos + j, y_pos] = ship_names.Keys.ElementAt(i);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < ship_names.Values.ElementAt(i); j++)
                            {
                                ship_array[x_pos, y_pos + j] = ship_names.Keys.ElementAt(i);
                            }
                        }
                        DisplayArray(ship_array, letter_array);
                        break;
                    }
                } 
            }
            return new char[,] { };
        }

        static void Attack_Check(char[,] array, Dictionary<char, int> ship_names)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    continue;
                }
            }
        }
    }
}
