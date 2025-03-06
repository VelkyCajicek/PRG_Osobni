using System;
using System.Collections.Generic;

public class Square
{
    public int XPos { get; set; }
    public int YPos { get; set; }
    public Piece Piece { get; set; }
    public string Icon { get; set; }

    public Square(int xPos, int yPos, Piece piece = null)
    {
        XPos = xPos;
        YPos = yPos;
        Piece = piece;
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        if (Piece == null)
        {
            Icon = " ";
        }
        else
        {
            Type pieceType = Piece.GetType();
            bool isWhite = Piece.IsWhite;
            var icons = new Dictionary<Type, (string black, string white)>
            {
                { typeof(Pawn), ("p", "P") },
                { typeof(Rook), ("r", "R") },
                { typeof(Knight), ("n", "N") },
                { typeof(Bishop), ("b", "B") },
                { typeof(King), ("k", "K") },
                { typeof(Queen), ("q", "Q") }
            };
            Icon = icons.TryGetValue(pieceType, out var iconPair) ? (isWhite ? iconPair.white : iconPair.black) : "?";
        }
    }
}