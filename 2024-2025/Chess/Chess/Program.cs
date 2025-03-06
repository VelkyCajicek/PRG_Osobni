using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    static Square[,] SetupBoard()
    {
        Square[,] board = new Square[8, 8];
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
                board[y, x] = new Square(x, y);

        // Black pieces 
        Piece[] blackPieces = {
            new Rook(false, 0, 0, board), new Knight(false, 1, 0, board), new Bishop(false, 2, 0, board),
            new King(false, 3, 0, board), new Queen(false, 4, 0, board), new Bishop(false, 5, 0, board),
            new Knight(false, 6, 0, board), new Rook(false, 7, 0, board)
        };
        for (int i = 0; i < blackPieces.Length; i++)
        {
            board[0, i].Piece = blackPieces[i];
            board[0, i].UpdateIcon();
        }
        for (int i = 0; i < 8; i++)
        {
            board[1, i].Piece = new Pawn(false, i, 1, board);
            board[1, i].UpdateIcon();
        }

        // White pieces 
        Piece[] whitePieces = {
            new Rook(true, 0, 7, board), new Knight(true, 1, 7, board), new Bishop(true, 2, 7, board),
            new King(true, 3, 7, board), new Queen(true, 4, 7, board), new Bishop(true, 5, 7, board),
            new Knight(true, 6, 7, board), new Rook(true, 7, 7, board)
        };
        for (int i = 0; i < whitePieces.Length; i++)
        {
            board[7, i].Piece = whitePieces[i];
            board[7, i].UpdateIcon();
        }
        for (int i = 0; i < 8; i++)
        {
            board[6, i].Piece = new Pawn(true, i, 6, board);
            board[6, i].UpdateIcon();
        }
        return board;
    }

    static void DisplayBoard(Square[,] chessBoard)
    {
        for (int i = 7; i >= 0; i--)
        {
            string row = "";
            for (int j = 0; j < 8; j++)
                row += chessBoard[i, j].Icon + (" | ");
            Console.Write($"{row}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{8 - i}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("A | B | C | D | E | F | G | H |");
        Console.ForegroundColor = ConsoleColor.White;
    }

    static Square GetPlayerMove(string userInput, Square[,] chessBoard)
    {
        Dictionary<char, int> alphabetDict = new Dictionary<char, int>
        {
            {'a', 0}, {'b', 1}, {'c', 2}, {'d', 3}, {'e', 4}, {'f', 5}, {'g', 6}, {'h', 7}
        };
        while (true)
        {
            if (userInput.Length < 2 || !alphabetDict.ContainsKey(userInput[0]) || !char.IsDigit(userInput[1])) Console.WriteLine("Invalid input");
            else break;
        }
            
        int x = alphabetDict[userInput[0]];
        int y = 8 - int.Parse(userInput[1].ToString());
        return chessBoard[y, x];
    }

    // Removes all X from board after move is made
    static void ClearPossiblePositions(Square[,] chessBoard)
    {
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (chessBoard[i, j].Icon == "X")
                    chessBoard[i, j].UpdateIcon();
    }

    static bool IsInCheck(Square[,] board, bool isWhite)
    {
        // This fixes None or something else
        int? kingX = null;
        int? kingY = null;

        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                Piece piece = board[y, x].Piece;
                if (piece != null && piece is King && piece.IsWhite == isWhite)
                {
                    kingX = x;
                    kingY = y;
                    break;
                }
            }
            if (kingX.HasValue) break;
        }
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                Piece piece = board[y, x].Piece;
                if (piece != null && piece.IsWhite != isWhite)
                {
                    List<int[]> moves = piece.GetPossibleMoves();
                    foreach (var move in moves)
                        if (move[0] == kingX && move[1] == kingY)
                            return true;
                }
            }
        return false;
    }

    static bool IsCheckmate(Square[,] board, bool isWhite)
    {
        if (!IsInCheck(board, isWhite)) return false;
        Piece king = null;
        int kingX = -1, kingY = -1;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                Piece piece = board[y, x].Piece;
                if (piece != null && piece is King && piece.IsWhite == isWhite)
                {
                    king = piece;
                    kingX = x;
                    kingY = y;
                    break;
                }
            }
            if (king != null) break;
        }
        List<int[]> kingMoves = king.GetPossibleMoves();
        foreach (var move in kingMoves)
        {
            int newX = move[0], newY = move[1];
            Piece originalPiece = board[newY, newX].Piece;
            board[newY, newX].Piece = king;
            board[kingY, kingX].Piece = null;
            king.CurrentXPosition = newX;
            king.CurrentYPosition = newY;

            bool stillInCheck = IsInCheck(board, isWhite);
            board[kingY, kingX].Piece = king;
            board[newY, newX].Piece = originalPiece;
            king.CurrentXPosition = kingX;
            king.CurrentYPosition = kingY;
            if (!stillInCheck) return false;
        }
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
            {
                Piece piece = board[y, x].Piece;
                if (piece != null && piece.IsWhite == isWhite && !(piece is King))
                {
                    List<int[]> moves = piece.GetPossibleMoves();
                    foreach (var move in moves)
                    {
                        int newX = move[0], newY = move[1];
                        Piece originalPiece = board[newY, newX].Piece;
                        board[newY, newX].Piece = piece;
                        board[y, x].Piece = null;
                        piece.CurrentXPosition = newX;
                        piece.CurrentYPosition = newY;

                        bool stillInCheck = IsInCheck(board, isWhite);
                        board[y, x].Piece = piece;
                        board[newY, newX].Piece = originalPiece;
                        piece.CurrentXPosition = x;
                        piece.CurrentYPosition = y;
                        if (!stillInCheck) return false;
                    }
                }
            }
        return true;
    }

    static bool UpdateBoard(Square chosenSquare, List<int[]> possibleMovesList, Square[,] chessBoard, int playerColor)
    {
        List<int[]> validMoves = new List<int[]>();
        Piece movingPiece = chosenSquare.Piece;
        foreach (var move in possibleMovesList)
        {
            int newX = move[0], newY = move[1];
            Piece originalPiece = chessBoard[newY, newX].Piece;
            chessBoard[newY, newX].Piece = movingPiece;
            chessBoard[chosenSquare.YPos, chosenSquare.XPos].Piece = null;
            if (movingPiece != null)
            {
                movingPiece.CurrentXPosition = newX;
                movingPiece.CurrentYPosition = newY;
            }
            if (!IsInCheck(chessBoard, playerColor == 0))
                validMoves.Add(new int[] { newX, newY });
            chessBoard[chosenSquare.YPos, chosenSquare.XPos].Piece = movingPiece;
            chessBoard[newY, newX].Piece = originalPiece;
            if (movingPiece != null)
            {
                movingPiece.CurrentXPosition = chosenSquare.XPos;
                movingPiece.CurrentYPosition = chosenSquare.YPos;
            }
            if (originalPiece != null)
            {
                originalPiece.CurrentXPosition = newX;
                originalPiece.CurrentYPosition = newY;
            }
        }

        if (validMoves.Count == 0)
        {
            Console.WriteLine("No legal moves available (would leave king in check)");
            return false;
        }

        foreach (var move in validMoves)
            chessBoard[move[1], move[0]].Icon = "X";
        Console.Clear();
        DisplayBoard(chessBoard);

        while (true)
        {
            Console.Write("Pick where you want to move this piece (or 'cancel'): ");
            string moveChoice = Console.ReadLine().ToLower();
            if (moveChoice == "cancel")
                break;
            try
            {
                Square newSquare = GetPlayerMove(moveChoice, chessBoard);
                if (newSquare.Icon == "X")
                {
                    newSquare.Piece = movingPiece;
                    if (newSquare.Piece != null)
                    {
                        newSquare.Piece.CurrentXPosition = newSquare.XPos;
                        newSquare.Piece.CurrentYPosition = newSquare.YPos;
                    }
                    chosenSquare.Piece = null;
                    newSquare.UpdateIcon();
                    chosenSquare.UpdateIcon();
                    break;
                }
                else
                    Console.WriteLine("Not a valid move, please choose another one");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid destination");
            }
        }
        ClearPossiblePositions(chessBoard);
        Console.Clear();
        DisplayBoard(chessBoard);
        return true;
    }

    static void Main(string[] args)
    {
        Square[,] board = SetupBoard();
        int playerColor = 0; // 0 = white, 1 = black
        DisplayBoard(board);

        while (true)
        {
            Console.Write("Would you like to be white or black? (Type 'white' or 'black'): ");
            string playerColorChoice = Console.ReadLine().ToLower();
            if (playerColorChoice == "white")
            {
                playerColor = 0;
                break;
            }
            else if (playerColorChoice == "black")
            {
                playerColor = 1;
                break;
            }
            else
                Console.WriteLine("Invalid input");
        }

        while (true)
        {
            Square chosenSquare = null;
            List<int[]> possibleMoves = new List<int[]>();

            while (true)
            {
                Console.Write("Which piece would you like to move? ");
                string userMove = Console.ReadLine().ToLower();
                try
                {
                    chosenSquare = GetPlayerMove(userMove, board);
                    if (chosenSquare.Piece == null)
                    {
                        Console.WriteLine("No piece at that square");
                        continue;
                    }
                    if ((chosenSquare.Piece.IsWhite && playerColor == 1) || (!chosenSquare.Piece.IsWhite && playerColor == 0))
                    {
                        Console.WriteLine("Not your color piece");
                        continue;
                    }
                    possibleMoves = chosenSquare.Piece.GetPossibleMoves();
                    if (possibleMoves.Count == 0)
                    {
                        Console.WriteLine("Can't move this piece");
                        continue;
                    }
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid move");
                }
            }

            if (UpdateBoard(chosenSquare, possibleMoves, board, playerColor))
            {
                int opponentColor = 1 - playerColor;
                if (IsInCheck(board, opponentColor == 0))
                {
                    Console.WriteLine("Check");
                    if (IsCheckmate(board, opponentColor == 0))
                    {
                        Console.WriteLine("Checkmate");
                        break;
                    }
                }
            }
        }
        Console.ReadKey();
    }
}