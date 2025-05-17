
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CardSlot
{
    public GameObject cardObject;
    public Button button;
    public Image image;
    public string cardName;
    public bool isActive = false;
}






public class Game : MonoBehaviour
{
    // Reference from Unity IDE
    public GameObject chesspiece;
    public GameObject chessBoard;
    public GameObject settings;

    // Matrices needed, positions of each of the GameObjects
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    // Current turn
    private string currentPlayer = "white";

    // Game Ending
    private bool gameOver = false;

    // ----- CARD SYSTEM -----
    public TextMeshProUGUI cardDisplay;
    public Button drawButton;
    private List<string> deck = new List<string>();

    public Button MenuButton;

    public CardSlot[] whiteCards = new CardSlot[3];
    public CardSlot[] blackCards = new CardSlot[3];




    public void Start()
    {
        // Initialize players
        playerWhite = new GameObject[] { Create("white_rook", 0, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1) };
        playerBlack = new GameObject[] { Create("black_rook", 0, 7), Create("black_knight",1,7),
            Create("black_bishop",2,7), Create("black_queen",3,7), Create("black_king",4,7),
            Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6) };

        // Set all piece positions on the board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }

        // ----- CARD SYSTEM SETUP -----
        //deck = new List<string> {  "Add 2 Pawns", "Queens Move Diagonally Only", "Swap a Knight and a Bishop", "Pawns Move Backward", "Instant Promotion", "Rook Teleport", "King’s Shield", "Bishop Frenzy", "Steal a Move", "Reverse Attack" };
        deck = new List<string> {
    "Add 2 Pawns", "Add Knight", "Add Bishop", "Add Rook", "Add Queen"
};
        drawButton.onClick.AddListener(DrawCard);

        InitializeCardSlots(whiteCards);
        InitializeCardSlots(blackCards);

    }


    void InitializeCardSlots(CardSlot[] slots)
    {
        foreach (var slot in slots)
        {
            SetCardSlotState(slot, false);
            slot.button.onClick.AddListener(() => OnCardClicked(slot));
        }
    }

    public void ReturnToMenu()
    {
        //SceneManager.LoadScene("Settings");
        chessBoard.SetActive(false);
        settings.SetActive(true);
    }
    public void CloseSettings()
    {
        chessBoard.SetActive(true);
        settings.SetActive(false);
    }





    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity, chessBoard.transform);
        obj.transform.localScale = Vector3.one * 1.2f;
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }


    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        return x >= 0 && y >= 0 && x < positions.GetLength(0) && y < positions.GetLength(1);
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        currentPlayer = (currentPlayer == "white") ? "black" : "white";
    }

    public void Update()
    {

        if (gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<TextMeshProUGUI>().text = playerWinner + " is the winner";
    }

    // ----- CARD DRAWING FUNCTION -----
    public void DrawCard()
    {
        if (deck.Count == 0)
        {
            cardDisplay.text = "Deck is empty!";
            return;
        }

        int randomIndex = Random.Range(0, deck.Count);
        string drawnCard = deck[randomIndex];
        deck.RemoveAt(randomIndex);

        cardDisplay.text = "You drew: " + drawnCard;

        CardSlot[] slots = (currentPlayer == "white") ? whiteCards : blackCards;

        foreach (var slot in slots)
        {
            if (string.IsNullOrEmpty(slot.cardName))
            {
                slot.cardName = drawnCard;
                slot.image.color = new Color(1f, 1f, 1f, 0.1f); // 10% visible
                slot.button.interactable = false;
                slot.cardObject.GetComponentInChildren<TextMeshProUGUI>().text = drawnCard;
                slot.cardObject.SetActive(true);
                break;
            }
        }
    }

    void OnCardClicked(CardSlot slot)
    {
        if (slot.isActive || string.IsNullOrEmpty(slot.cardName)) return;

        slot.isActive = true;
        SetCardSlotState(slot, true);
        ApplyCardEffect(slot.cardName);
        slot.cardName = ""; // Clear used card
    }

    void SetCardSlotState(CardSlot slot, bool active)
    {
        slot.button.interactable = active;
        slot.image.color = active ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0.1f);
    }


    private void ApplyCardEffect(string card)
    {
        switch (card)
        {
            case "Add 2 Pawns":
                AddTwoPawns(currentPlayer);
                break;
            case "Add Knight":
                AddRandomPiece(currentPlayer, "knight");
                break;
            case "Add Bishop":
                AddRandomPiece(currentPlayer, "bishop");
                break;
            case "Add Rook":
                AddRandomPiece(currentPlayer, "rook");
                break;
            case "Add Queen":
                AddRandomPiece(currentPlayer, "queen");
                break;
            default:
                Debug.Log("No effect implemented for: " + card);
                break;
        }
    }


    private void AddTwoPawns(string player)
    {
        int y = (player == "white") ? 1 : 6;
        int added = 0;

        for (int x = 0; x < 8 && added < 2; x++)
        {
            if (positions[x, y] == null)
            {
                GameObject pawn = Create(player + "_pawn", x, y);
                SetPosition(pawn);

                if (player == "white")
                    playerWhite = AddToArray(playerWhite, pawn);
                else
                    playerBlack = AddToArray(playerBlack, pawn);

                added++;
            }
        }

        Debug.Log(player + " gained 2 pawns!");
    }
    private void AddRandomPiece(string player, string pieceType)
    {
        List<Vector2Int> emptySpots = new List<Vector2Int>();

        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (positions[x, y] == null)
                    emptySpots.Add(new Vector2Int(x, y));
            }
        }

        if (emptySpots.Count == 0)
        {
            Debug.Log("No empty positions to add a new piece.");
            return;
        }

        Vector2Int chosenSpot = emptySpots[Random.Range(0, emptySpots.Count)];
        string pieceName = player + "_" + pieceType;
        GameObject newPiece = Create(pieceName, chosenSpot.x, chosenSpot.y);
        SetPosition(newPiece);

        if (player == "white")
            playerWhite = AddToArray(playerWhite, newPiece);
        else
            playerBlack = AddToArray(playerBlack, newPiece);

        Debug.Log($"{player} added a {pieceType} at ({chosenSpot.x}, {chosenSpot.y})");
    }

    private GameObject[] AddToArray(GameObject[] original, GameObject toAdd)
    {
        GameObject[] newArray = new GameObject[original.Length + 1];
        original.CopyTo(newArray, 0);
        newArray[original.Length] = toAdd;
        return newArray;
    }




}
