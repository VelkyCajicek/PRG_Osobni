using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProjectCompensation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ahoj Honzo, jenom pro tvoje info, vytvoril jsem tohle protoze jsem ostuda a nestihl jsem rozfungovat sachy
            //string[,] board = new string[3, 3]; board = FillBoard(board);
            char Player = 'x';
            char Opponent = 'o';
            //DisplayBoard(board);
            //Console.WriteLine(GameState(board));
            Console.WriteLine(int.MinValue);
            int turn = 0;
            bool isMax = true;

            string[,] board = { 
                { "x", "x", "3" }, 
                { "o", "o", "6" }, 
                { "7", "x", "9" } 
            };
            Console.WriteLine(Minimax(board, 'o'));
            Console.ReadKey();
        }
        static void DisplayBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write("{");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(" " + board[i, j] + " ");
                }
                Console.Write("}\n");
            }
        }
        static string[,] FillBoard(string[,] board)
        {
            int number = 1;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = number.ToString();
                    number++;
                }
            }
            return board;
        }
        static bool MovesLeft(string[,] board)
        {
            List<string> nums = new List<string>() { "1" , "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (nums.Contains(board[i,j])) { return true; }
                }
            }
            return false;
        }
        static int GameState(string[,] board)
        {
            if (MovesLeft(board) == false) { return 0; }
            int currentState = Row(board) + Col(board) + mainDiagonal(board) + secondDiagonal(board);
            return currentState;
        }
        static int Row(string[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                string value = board[i, 0];
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] != value) { break; }
                    if(j == 2) { return Winner(board, value); }
                }
            }
            return 0;
        }
        static int Col(string[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                string value = board[0, i];
                for (int j = 0; j < 3; j++)
                {
                    if (board[j, i] != value) { break; }
                    if (j == 2) { return Winner(board, value); }
                }
            }
            return 0;
        }
        static int mainDiagonal(string[,] board)
        {
            string value = "";
            for (int i = 0; i < 3; i++)
            {
                value = board[0, 0];
                if (board[i,i] != value) { return 0; }
            }
            return Winner(board, value);
        }
        static int secondDiagonal(string[,] board)
        {
            string value = "";
            for (int i = 0; i < 3; i++)
            {
                value = board[0, 2];
                if (board[0+i, 2-i] != value) { return 0; }
            }
            return Winner(board, value);
        }
        static int Max(int num1, int num2)
        {
            if (num1 > num2)
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
            if (num1 < num2)
            {
                return num1;
            }
            else
            {
                return num2;
            }
        }
        static int Winner(string[,] board, string value)
        {
            if(value == "o")
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        static bool invertBool(bool value)
        {
            if (value == true) { return false; }
            return true;
        }
        static int Minimax(string [,] board, char turnIcon)
        {
            var score = GameState(board);
            if (score != 0)
            {
                return score;
            }

            var bestScore = turnIcon == 'o' ? int.MinValue : int.MaxValue;

            int CalcBest(int x, int y) => (turnIcon == 'o' ? x > y : y > x) ? x : y;

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (board[i, j] != "o" && board[i, j] != "x")
                    {
                        
                        string temp = board[i, j];
                        int currentScore = Minimax(board, turnIcon == 'o' ? 'x' : 'o');
                        board[i, j] = temp;

                        bestScore = CalcBest(bestScore, currentScore);
                    }
                }
            }
            return bestScore;
        }
        static int[] findBestMove(string[,] board, string Player)
        {
            int bestValue = int.MinValue;
            int[] bestMove = new int[2] { -1, -1 };
            List<string> nums = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (nums.Contains(board[i, j])) { }
                    else
                    {
                        string temp = board[i, j];
                        board[i, j] = Player;
                        int moveValue = Minimax(board, '0');
                        board[i,j] = temp;
                        if(moveValue < bestValue)
                        {
                            bestMove[0] = i; bestMove[1] = j;
                            bestValue = moveValue;
                        }
                    }
                }
            }
            return bestMove;
        }
    }
}
