using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.IO;
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
            Dictionary<string, int> positions = new Dictionary<string, int>()
            {
                { "1", 7 }, {"2", 6 }, {"3",5}, {"4", 4}, {"5", 3}, {"6", 2}, {"7", 1}, {"8", 0},
                { "H", 7 }, {"G", 6 }, {"F",5}, {"E", 4}, {"D", 3}, {"C", 2}, {"B", 1}, {"A", 0}
            };
            string userInput;
            string[,] board = new string[8,8]; fillBoard(board);
            //userInput = "A1H8";
            // Setting up the board
            // White pieces
            Rook WhiteRook1 = new Rook(7, 7, 'W', board);
            Rook WhiteRook2 = new Rook(7, 0, 'W', board);
            Knight WhiteKnight1 = new Knight(7, 6, 'W', board);
            Knight WhiteKnight2 = new Knight(7, 1, 'W', board);
            Bishop WhiteBishop1 = new Bishop(7, 2, 'W' ,board);
            Bishop WhiteBishop2 = new Bishop(7, 5, 'W', board);
            King WhiteKing = new King(7, 3, 'W', board);
            Queen WhiteQueen = new Queen(7, 4, 'W', board);
            // Black pieces
            Rook BlackRook1 = new Rook(0, 7, 'B', board);
            Rook BlackRook2 = new Rook(0, 0, 'B', board);
            Knight BlackKnight1 = new Knight(0, 6, 'B', board);
            Knight BlackKnight2 = new Knight(0, 1, 'B', board);
            Bishop BlackBishop1 = new Bishop(0, 2, 'B', board);
            Bishop BlackBishop2 = new Bishop(0, 5, 'B', board);
            King BlackKing = new King(0, 3, 'B', board);
            Queen BlackQueen = new Queen(0, 4, 'B', board);
            //Pawns
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Pawn WhitePawn = new Pawn(6, i, 'W', board);
                Pawn BlackPawn = new Pawn(1, i, 'B', board);
            }
            //DisplayBoard(board);
            //Game
            int turn = 0;
            // Validate user input
            while (true)
            {
                while (true)
                {
                    Console.WriteLine("This is the current board:");
                    DisplayBoard(board);
                    Console.WriteLine("Enter your move");
                    userInput = Console.ReadLine();
                    if (validateUserInput(userInput, positions) == true) { break; }
                    else { Console.WriteLine("Position doesnt exist"); }
                }
                while (true)
                {
                    // First position coordinates
                    int x = positions[userInput[0].ToString()]; int y = positions[userInput[1].ToString()];
                    // Second position coordinates
                    int a = positions[userInput[2].ToString()]; int b = positions[userInput[3].ToString()];
                    // Info about current piece
                    string pieceType = board[x, y][1].ToString(); string pieceColor = board[x, y][0].ToString();

                    Console.WriteLine($"{x} {y} {a} {b}");
                    Console.WriteLine(board[x, y]);

                    if (LegalMove(board,turn, pieceColor, pieceType, x, y, a, b) == false) { Console.WriteLine("Not a legal move"); break; }
                    else
                    {
                        ChessPieces.Move(x, y, a, b, board); turn++; break;
                    }
                }
            }
            Console.ReadKey();
        }
        static bool validateUserInput(string userInput, Dictionary<string, int> positions)
        {
            if(userInput.Length != 4) return false;
            for(int i = 0; i < userInput.Length; i++)
            {
                if (positions.ContainsKey(userInput[i].ToString()) == false){ return false; }
            }
            return true;
        }
        static void DisplayBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(" " + board[i, j] + " ");
                }
                Console.Write("\n");
            }
        }
        static string[,] fillBoard(string[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = "00";
                }
            }
            return board;
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
        static bool RookMove(int x, int y, int a, int b, string[,] board)
        {
            if (x == a)
            {
                int axis = x;
                int end_point = Max(y, b);
                int start_point = Min(y, b);
                for (int i = start_point + 1; i < end_point; i++)
                {
                    if (board[axis, i] != "00") { return false; }
                }
                return true;
            }
            if (y == b)
            {
                int axis = y;
                int end_point = Max(x, a);
                int start_point = Min(x, a);
                for (int i = start_point + 1; i < end_point; i++)
                {
                    if (board[i, axis] != "00") { return false; }
                }
                return true;
            }
            else { return false; }
        }
        static bool BishopMove(int x, int y, int a, int b, string[,] board)
        {
            List<string> path = new List<string>();
            bool check1 = false;
            bool check2 = false;

            if ((a - x) >= 0) { check1 = true; }
            if ((b - y) >= 0) { check2 = true; }

            if (Math.Abs((x - a) / (y - b)) != 1) { return false; }
            if (check1 && check2)
            {
                for (int i = 0; i < (a - x - 1); i++) { path.Add(board[x + 1 + i, y + 1 + i]); }
            }
            if (check1 && check2 == false)
            {
                for (int i = 0; i < (a - x - 1); i++) { path.Add(board[x + 1 + i, y + 1 - i]); }
            }
            if (check1 == false && check2)
            {
                for (int i = 0; i < (a - x - 1); i++) { path.Add(board[x + 1 - i, y + 1 + i]); }
            }
            if (check2 == false && check1 == false)
            {
                for (int i = 0; i < (a - x - 1); i++) { path.Add(board[x + 1 - i, y + 1 - i]); }
            }
            for (int i = 0; i < path.Count(); i++)
            {
                if (path[i] != "00") { return false; }
            }
            return true;
        }
        static bool KingMove(int x, int y, int a, int b, string[,] board)
        {
            if (Math.Abs(x - a) > 1) { return false; }
            if (Math.Abs(y - b) > 1) { return false; }
            return true;
        }
        static bool QueenMove(int x, int y, int a, int b, string[,] board)
        {
            List<string> path = new List<string>();
            bool check1 = false;
            bool check2 = false;

            if ((a - x) >= 0) { check1 = true; }
            if ((b - y) >= 0) { check2 = true; }

            if ((Math.Abs((x - a) / (y - b)) != 1) || ((x - a) != 0) || ((y - b) != 0))
            {
                return false;
            }
            if (x - a == 0)
            {
                if (check2) { }
            }
            for (int i = 0; i < path.Count(); i++)
            {
                if (path[i] != "00") { return false; }
            }
            return true;
        }
        static bool KnightMove(int x, int y, int a, int b, string[,] board)
        {
            if(((Math.Abs(x-a) != 2) && (Math.Abs(y-b) != 1)) || ((Math.Abs(x-a) != 1) && (Math.Abs(y-b) != 2)))
            {
                return false;
            }
            return true;
        }
        static bool PawnMove(int x, int y, int a, int b, string[,] board, string pieceColor)
        {
            if(pieceColor == "W")
            {
                if (y - b != 1) { return false; }
                else {  return true; }
            }
            else
            {
                if (y - b != -1) { return false; }
                else { return true; }
            }
        }
        static bool PieceColorCheck(int x, int y, int a, int b, string[,] board)
        {
            if(board[x, y][0].ToString() == board[a, b][0].ToString()) { return true; }
            return false;
        }
        static bool LegalMove(string[,] board, int turn, string pieceColor, string pieceType, int x, int y, int a, int b)
        {
            if (turn % 2 == 0) { if (pieceColor == "B") { return false; } }
            else { if(pieceColor == "W") { return false; } }

            int checkNewPosition = 0;
            int oppenentKing = 0;

            

            if (board[a, b] != "00")
            {
                if (PieceColorCheck(x, y, a, b, board) == true) { return false; }
            }
            switch(pieceType){
                case "K": return KingMove(x, y, a, b, board);
                case "Q": return QueenMove(x, y, a, b, board);
                case "B": return BishopMove(x, y, a, b, board);
                case "R": return RookMove(x, y, a, b, board);
                case "N": return KnightMove(x, y, a, b, board);
                case "P": return PawnMove(x, y, a, b, board, pieceColor);
                default: break;
            }
            return true;
        }
    }
    class ChessPieces
    {
        public int x;
        public int y;
        public char color;
        public string piece;
        public static void Move(int x, int y, int a, int b, string[,] board)
        {
            board[a,b] = board[x,y];
            board[x,y] = "00";
        }
    }
    class Rook : ChessPieces
    {
        public char icon = 'R';
        public Rook(int x, int y, char color, string [,] board)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            board[x,y] = color + icon.ToString();
        }
    }
    class Knight : ChessPieces
    {
        public char icon = 'N';
        public Knight(int x, int y, char color, string[,] board)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            board[x, y] = color + icon.ToString();
        }
    }
    class Bishop : ChessPieces
    {
        public char icon = 'B';
        public Bishop(int x, int y, char color, string[,] board)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            board[x, y] = color + icon.ToString();
        }
    }
    class Queen : ChessPieces
    {
        public char icon = 'Q';
        public Queen(int x, int y, char color, string[,] board)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            board[x, y] = color + icon.ToString();
        }
    }
    class King : ChessPieces
    {
        public char icon = 'K';
        public King(int x, int y, char color, string[,] board)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            board[x, y] = color + icon.ToString();
        }
    }
    class Pawn : ChessPieces
    {
        public char icon = 'P';
        public Pawn(int x, int y, char color, string[,] board)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            board[x, y] = color + icon.ToString();
        }
    }
}
