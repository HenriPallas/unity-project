using System.Collections.Generic;

[System.Serializable]
public class ChessPieceData
{
    public string name;
    public int x;  // must match JSON key "x"
    public int y;  // must match JSON key "y"
}

[System.Serializable]
public class ChessPieceDataList
{
    public List<ChessPieceData> pieces;
    public string currentPlayer;
}
