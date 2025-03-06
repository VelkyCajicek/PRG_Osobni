using System;
using System.Collections.Generic;

public abstract class Piece
{
    public bool IsWhite { get; set; }
    public int CurrentXPosition { get; set; }
    public int CurrentYPosition { get; set; }
    protected Square[,] Board { get; set; }

    public Piece(bool isWhite, int currentXPosition, int currentYPosition, Square[,] board)
    {
        IsWhite = isWhite;
        CurrentXPosition = currentXPosition;
        CurrentYPosition = currentYPosition;
        Board = board;
    }

    public abstract List<int[]> GetPossibleMoves();
}

public class Rook : Piece
{
    public Rook(bool isWhite, int currentXPosition, int currentYPosition, Square[,] board)
        : base(isWhite, currentXPosition, currentYPosition, board) { }

    public override List<int[]> GetPossibleMoves()
    {
        List<int[]> moves = new List<int[]>();
        int[,] directions = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int differenceX = directions[i, 0];
            int differenceY = directions[i, 1];
            int nx = CurrentXPosition;
            int ny = CurrentYPosition;
            while (true)
            {
                nx += differenceX;
                ny += differenceY;
                if (nx >= 0 && nx < 8 && ny >= 0 && ny < 8)
                {
                    if (Board[ny, nx].Piece == null)
                        moves.Add(new int[] { nx, ny });
                    else if (Board[ny, nx].Piece.IsWhite != IsWhite)
                    {
                        moves.Add(new int[] { nx, ny });
                        break;
                    }
                    else
                        break;
                }
                else
                    break;
            }
        }
        return moves;
    }
}

public class Knight : Piece
{
    public Knight(bool isWhite, int currentXPosition, int currentYPosition, Square[,] board)
        : base(isWhite, currentXPosition, currentYPosition, board) { }

    public override List<int[]> GetPossibleMoves()
    {
        List<int[]> moves = new List<int[]>();
        int[,] knightMoves = { { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 }, { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 } };

        for (int i = 0; i < knightMoves.GetLength(0); i++)
        {
            int nx = CurrentXPosition + knightMoves[i, 0];
            int ny = CurrentYPosition + knightMoves[i, 1];
            if (nx >= 0 && nx < 8 && ny >= 0 && ny < 8)
            {
                if (Board[ny, nx].Piece == null || Board[ny, nx].Piece.IsWhite != IsWhite)
                    moves.Add(new int[] { nx, ny });
            }
        }
        return moves;
    }
}

public class Bishop : Piece
{
    public Bishop(bool isWhite, int currentXPosition, int currentYPosition, Square[,] board)
        : base(isWhite, currentXPosition, currentYPosition, board) { }

    public override List<int[]> GetPossibleMoves()
    {
        List<int[]> moves = new List<int[]>();
        int[,] directions = { { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int differenceX = directions[i, 0];
            int differenceY = directions[i, 1];
            int nx = CurrentXPosition;
            int ny = CurrentYPosition;
            while (true)
            {
                nx += differenceX;
                ny += differenceY;
                if (nx >= 0 && nx < 8 && ny >= 0 && ny < 8)
                {
                    if (Board[ny, nx].Piece == null)
                        moves.Add(new int[] { nx, ny });
                    else if (Board[ny, nx].Piece.IsWhite != IsWhite)
                    {
                        moves.Add(new int[] { nx, ny });
                        break;
                    }
                    else
                        break;
                }
                else
                    break;
            }
        }
        return moves;
    }
}

public class King : Piece
{
    public King(bool isWhite, int currentXPosition, int currentYPosition, Square[,] board)
        : base(isWhite, currentXPosition, currentYPosition, board) { }

    public override List<int[]> GetPossibleMoves()
    {
        List<int[]> moves = new List<int[]>();
        int[,] kingMoves = { { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, 1 }, { 0, -1 }, { -1, 1 }, { -1, 0 }, { -1, -1 } };

        for (int i = 0; i < kingMoves.GetLength(0); i++)
        {
            int nx = CurrentXPosition + kingMoves[i, 0];
            int ny = CurrentYPosition + kingMoves[i, 1];
            if (nx >= 0 && nx < 8 && ny >= 0 && ny < 8)
            {
                if (Board[ny, nx].Piece == null || Board[ny, nx].Piece.IsWhite != IsWhite)
                    moves.Add(new int[] { nx, ny });
            }
        }
        return moves;
    }
}

public class Queen : Piece
{
    private Rook rookMoves;
    private Bishop bishopMoves;

    public Queen(bool isWhite, int currentXPosition, int currentYPosition, Square[,] board)
        : base(isWhite, currentXPosition, currentYPosition, board)
    {
        rookMoves = new Rook(isWhite, currentXPosition, currentYPosition, board);
        bishopMoves = new Bishop(isWhite, currentXPosition, currentYPosition, board);
    }

    public override List<int[]> GetPossibleMoves()
    {
        rookMoves.CurrentXPosition = CurrentXPosition;
        rookMoves.CurrentYPosition = CurrentYPosition;
        bishopMoves.CurrentXPosition = CurrentXPosition;
        bishopMoves.CurrentYPosition = CurrentYPosition;
        List<int[]> moves = rookMoves.GetPossibleMoves();
        moves.AddRange(bishopMoves.GetPossibleMoves());
        return moves;
    }
}

public class Pawn : Piece
{
    public Pawn(bool isWhite, int currentXPosition, int currentYPosition, Square[,] board) : base(isWhite, currentXPosition, currentYPosition, board) { }

    public override List<int[]> GetPossibleMoves()
    {
        List<int[]> moves = new List<int[]>();
        int direction = IsWhite ? -1 : 1;
        int ny = CurrentYPosition + direction;

        if (ny >= 0 && ny < 8 && Board[ny, CurrentXPosition].Piece == null)
        {
            moves.Add(new int[] { CurrentXPosition, ny });
            if ((IsWhite && CurrentYPosition == 6) || (!IsWhite && CurrentYPosition == 1))
            {
                int ny2 = CurrentYPosition + 2 * direction;
                if (ny2 >= 0 && ny2 < 8 && Board[ny2, CurrentXPosition].Piece == null)
                    moves.Add(new int[] { CurrentXPosition, ny2 });
            }
        }

        int[] differenceXs = { -1, 1 };
        foreach (int differenceX in differenceXs)
        {
            int nx = CurrentXPosition + differenceX;
            ny = CurrentYPosition + direction;
            if (nx >= 0 && nx < 8 && ny >= 0 && ny < 8)
            {
                if (Board[ny, nx].Piece != null && Board[ny, nx].Piece.IsWhite != IsWhite)
                    moves.Add(new int[] { nx, ny });
            }
        }
        return moves;
    }
}