using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private string player;
    private string type;

    private Game game; // Reference to the Game script

    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;

    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; type = "queen"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; type = "knight"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; type = "bishop"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; type = "king"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; type = "rook"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; type = "pawn"; break;

            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; type = "queen"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; type = "knight"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; type = "bishop"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; type = "king"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; type = "rook"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; type = "pawn"; break;
        }
    }

    public void SetGameReference(Game gameRef)
    {
        game = gameRef;
    }

    public void DestroyMovePlates()
{
    GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");

    foreach (GameObject mp in movePlates)
    {
        Destroy(mp);
    }
}


    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    public int GetXBoard() => xBoard;
    public int GetYBoard() => yBoard;

    public string GetPlayer() => player;
    public string GetPieceType() => type;

    // Placeholder: in your actual move logic you will reference card effects like:
    public List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();

        string cardEffect = game.GetCurrentCardEffect();

        if (type == "pawn")
        {
            int direction = (player == "white") ? 1 : -1;

            // Card: "Pawns Move Backward"
            if (cardEffect == "Pawns Move Backward")
                direction *= -1;

            int nextY = yBoard + direction;
            if (game.PositionOnBoard(xBoard, nextY) && game.GetPosition(xBoard, nextY) == null)
                moves.Add(new Vector2Int(xBoard, nextY));
        }

        else if (type == "queen")
        {
            if (cardEffect == "Queens Move Diagonally Only")
            {
                AddDiagonalMoves(ref moves);
            }
            else
            {
                AddDiagonalMoves(ref moves);
                AddStraightMoves(ref moves);
            }
        }

        else if (type == "bishop")
        {
            AddDiagonalMoves(ref moves);
            if (cardEffect == "Bishop Frenzy")
            {
                AddDiagonalMoves(ref moves); // duplicated to exaggerate
            }
        }

        else if (type == "rook")
        {
            if (cardEffect == "Rook Teleport")
            {
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if ((x == xBoard || y == yBoard) && game.GetPosition(x, y) == null)
                        {
                            moves.Add(new Vector2Int(x, y));
                        }
                    }
                }
            }
            else
            {
                AddStraightMoves(ref moves);
            }
        }

        else if (type == "king")
        {
            AddKingMoves(ref moves);
            if (cardEffect == "King’s Shield")
            {
                // Logic handled elsewhere: can't be captured
            }
        }

        else if (type == "knight")
        {
            AddKnightMoves(ref moves);
        }

        return moves;
    }

    private void AddDiagonalMoves(ref List<Vector2Int> moves)
    {
        int[] dx = { 1, 1, -1, -1 };
        int[] dy = { 1, -1, 1, -1 };

        for (int dir = 0; dir < 4; dir++)
        {
            int x = xBoard + dx[dir];
            int y = yBoard + dy[dir];

            while (game.PositionOnBoard(x, y))
            {
                if (game.GetPosition(x, y) == null)
                    moves.Add(new Vector2Int(x, y));
                else if (game.GetPosition(x, y).GetComponent<Chessman>().GetPlayer() != player)
                {
                    moves.Add(new Vector2Int(x, y));
                    break;
                }
                else break;

                x += dx[dir];
                y += dy[dir];
            }
        }
    }

    private void AddStraightMoves(ref List<Vector2Int> moves)
    {
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };

        for (int dir = 0; dir < 4; dir++)
        {
            int x = xBoard + dx[dir];
            int y = yBoard + dy[dir];

            while (game.PositionOnBoard(x, y))
            {
                if (game.GetPosition(x, y) == null)
                    moves.Add(new Vector2Int(x, y));
                else if (game.GetPosition(x, y).GetComponent<Chessman>().GetPlayer() != player)
                {
                    moves.Add(new Vector2Int(x, y));
                    break;
                }
                else break;

                x += dx[dir];
                y += dy[dir];
            }
        }
    }

    private void AddKingMoves(ref List<Vector2Int> moves)
    {
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                int x = xBoard + dx;
                int y = yBoard + dy;

                if (game.PositionOnBoard(x, y))
                {
                    GameObject target = game.GetPosition(x, y);
                    if (target == null || target.GetComponent<Chessman>().GetPlayer() != player)
                    {
                        moves.Add(new Vector2Int(x, y));
                    }
                }
            }
        }
    }

    private void AddKnightMoves(ref List<Vector2Int> moves)
    {
        int[] dx = { 1, 2, 2, 1, -1, -2, -2, -1 };
        int[] dy = { 2, 1, -1, -2, -2, -1, 1, 2 };

        for (int i = 0; i < 8; i++)
        {
            int x = xBoard + dx[i];
            int y = yBoard + dy[i];

            if (game.PositionOnBoard(x, y))
            {
                GameObject target = game.GetPosition(x, y);
                if (target == null || target.GetComponent<Chessman>().GetPlayer() != player)
                {
                    moves.Add(new Vector2Int(x, y));
                }
            }
        }
    }
}
