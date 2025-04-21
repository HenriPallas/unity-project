using System;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<PieceInfo> pieces;
    public string currentPlayer;

    public SaveData(Chessman[,] board, string player)
    {
        currentPlayer = player;
        pieces = new List<PieceInfo>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                Chessman piece = board[x, y];
                if (piece != null)
                {
                    // Remove the "(Clone)" part if it exists
                    string cleanName = piece.name.Replace("(Clone)", "").Trim();

                    pieces.Add(new PieceInfo
                    {
                        name = cleanName,
                        x = x,
                        y = y
                    });

                }
            }
        }
    }
}


[System.Serializable]
public class PieceInfo
{
    public string name;
    public int x;
    public int y;
}