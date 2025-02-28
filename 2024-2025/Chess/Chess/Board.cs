using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class Board
    {
        public int turn = 0;
        public bool playerIsWhite;

        void SetupBoard()
        {
            Console.WriteLine("R = Rook, N = Knight, B = Bishop, K = King, Q = Queen, P = Pawn");


            Console.WriteLine("----------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("List of pieces captured: ");
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"It's {getTurn()}'s turn");
            Console.ForegroundColor = ConsoleColor.White;
            if((this.playerIsWhite && getTurn() == "white") || (!this.playerIsWhite && getTurn() == "black"))
            {
                Console.WriteLine("Select a piece to move");
            }
        }
        string getTurn()
        {
            if(turn % 2 == 0)
            {
                return "black";
            }
            else
            {
                return "white";
            }
        }
        public Board(bool playerIsWhite)
        {
            this.playerIsWhite = playerIsWhite;
            this.SetupBoard();
        }
    }
}
