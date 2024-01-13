using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            string[,] board = {
                {"BR", "BN", "BB", "BQ", "BK", "BB", "BN", "BR" },
                {"BP" ,"BP","BP","BP","BP","BP","BP","BP" },
                {"E","E","E","E","E","E","E","E" },
                {"E","E","E","E","E","E","E","E" },
                {"E","E","E","E","E","E","E","E" },
                {"E","E","E","E","E","E","E","E" },
                {"WP","WP","WP","WP","WP","WP","WP","WP", },
                {"WR", "WN", "WB", "WQ", "WK", "WB", "WN", "WR" }
            };
            */
            string[,] board = {
                {"E","E","E","E","E","E","E","E" },
                {"E","E","E","E","E","E","E","E" },
                {"E","E","E","E","E","E","E","E" },
                {"E","E","E","BB","E","E","E","E" },
                {"E","E","E","E","E","E","E","E" },
                {"E","E","E","E","E","E","E","E" },
                {"E","E","E","E","E","E","E","E" },
                {"WR","E","E","E","E","E","E","E" },
            };
            Dictionary<string, int> positions = new Dictionary<string, int>()
            {
                { "1", 7 }, {"2", 6 }, {"3",5}, {"4", 4}, {"5", 3}, {"6", 2}, {"7", 1}, {"8", 0},
                { "H", 7 }, {"G", 6 }, {"F",5}, {"E", 4}, {"D", 3}, {"C", 2}, {"B", 1}, {"A", 0}
            };
            string testInput = "A1-A6";
            
            int[] firstPosition = new int[2]; int[] secondPosition = new int[2];
            firstPosition[0] = positions[testInput[1].ToString()]; firstPosition[1] = positions[testInput[0].ToString()];
            secondPosition[0] = positions[testInput[4].ToString()]; secondPosition[1] = positions[testInput[3].ToString()];

            //var firstPosition = Tuple.Create(positions[testInput[1].ToString()], positions[testInput[0].ToString()]);
            //var secondPosition = Tuple.Create(positions[testInput[4].ToString()], positions[testInput[3].ToString()]);
            
            if(getPieceInfo(board, firstPosition, secondPosition) == true) { board = Move(board, firstPosition, secondPosition); displayBoard(board); }
            else { Console.WriteLine("Move not possible"); }               
            Console.ReadKey();
        }
        static int Max(int num1,  int num2)
        {
            if(num1 > num2)
            {
                return num1;
            }
            else
            {
                return num2;
            }
        } 
        static int Min(int num1, int num2) 
        {
            if( num1 < num2)
            {
                return num1;
            }
            else
            {
                return num2;
            }
        }
        static bool Rook(string[,] board, dynamic firstPosition, dynamic secondPosition)
        {
            if (firstPosition[0] == secondPosition[0])
            {
                int axis = firstPosition[0];
                int end_point = Max(firstPosition[1], secondPosition[1]);
                int start_point = Min(firstPosition[1], secondPosition[1]);
                for (int i = start_point+1; i < end_point; i++)
                {
                    if (board[axis, i] != "E") { return false; }
                }
                return true;
            }
            if (firstPosition[1] == secondPosition[1])
            {
                int axis = firstPosition[1];
                int end_point = Max(firstPosition[0], secondPosition[0]);
                int start_point = Min(firstPosition[0], secondPosition[0]);
                for (int i = start_point+1; i < end_point; i++)
                {
                    if (board[i, axis] != "E") { return false; }
                }
                return true;
            }
            else { return false; }
        }
        static bool Bishop(string[,] board, dynamic firstPosition, dynamic secondPosition)
        {
            if (firstPosition[0] < secondPosition[0] && firstPosition[1] < secondPosition[1])
            {
                for (int i = firstPosition[0]; i < secondPosition[0]; i++)
                {
                    if (board[i,i] != "E") { return false; }
                }
                return true;
            }
            return false;
        }
        
        static bool King(string[,] board, dynamic firstPosition, dynamic secondPosition)
        {
            bool check1 = false;
            bool check2 = false;
            if ((firstPosition[0] + 1 == secondPosition[0]) || (firstPosition[0] - 1 == secondPosition[0]))
            {
                check1 = true;
            }
            else { return fa}
            return false;
        }

        static bool getPieceInfo(string[,] board, dynamic firstPosition, dynamic secondPosition)
        {
            string piece = board[firstPosition[0], firstPosition[1]];
            switch (piece[1])
            {
                case 'P': return false;
                case 'N': return false;
                case 'B': return false;
                case 'R': if (Rook(board, firstPosition, secondPosition) == true) { return true; } return false;
                default: return false;
            }
        }
        static string[,] Move(string[,] board, dynamic firstPosition, dynamic secondPosition)
        {
            string temp = board[secondPosition[0], secondPosition[1]];
            board[secondPosition[0], secondPosition[1]] = board[firstPosition[0], firstPosition[1]];
            if(temp != "E") { board[firstPosition[0], firstPosition[1]] = "E"; }
            else { board[firstPosition[0], firstPosition[1]] = temp; }
            return board;
        }
        static void displayBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
