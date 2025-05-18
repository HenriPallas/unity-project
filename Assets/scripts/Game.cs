
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
    public string cardName;

     public Image image;
     public Button button;

    public TextMeshProUGUI topText;
    public TextMeshProUGUI bottomText;
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

    private int extraTurnActive = 0;

    // Game Ending
    private bool gameOver = false;

    // ----- CARD SYSTEM -----
    public TextMeshProUGUI cardDisplay;
    public Button drawButton;
    private List<string> deck = new List<string>();

    public CardSlot[] whiteCards = new CardSlot[3];
    public CardSlot[] blackCards = new CardSlot[3];

    private int whiteTurnCounter = 0;
    private int blackTurnCounter = 0;

    // Set this to how many turns before drawing a card
    private const int turnsBeforeDraw = 5;

    public TextMeshProUGUI whiteTurnText;
    public TextMeshProUGUI blackTurnText;

    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool timerRunning = true;

    public GameObject gameOverObject;
    public TextMeshProUGUI winnerText;

    public Sprite spawnCardSprite;
    public Sprite tacticCardSprite;


    public TextMeshProUGUI sound;
    public TextMeshProUGUI music;

    private bool isSoundOn = true;
    private bool isMusicOn = true;



    public void Start()
    {
        Time.timeScale = 1;
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
        // Pawns
        "Add 2 Pawns", "Add 2 Pawns", "Add 2 Pawns", "Add 2 Pawns", "Add 2 Pawns",  "Add 2 Pawns", "Add 2 Pawns", "Add 2 Pawns", "Add 2 Pawns", "Add 2 Pawns", "Add 2 Pawns", 

        // Knights
        "Add Knight", "Add Knight", "Add Knight", "Add Knight", "Add Knight", "Add Knight", "Add Knight", "Add Knight",

        // Bishops
        "Add Bishop", "Add Bishop", "Add Bishop", "Add Bishop", "Add Bishop", "Add Bishop",  "Add Bishop", "Add Bishop",

        // Rooks
        "Add Rook", "Add Rook", "Add Rook", "Add Rook", "Add Rook", "Add Rook",

        // Queens
        "Add Queen", "Add Queen", "Add Queen", 

        // Skip Turn
        "Skip Opponent's Turn", "Skip Opponent's Turn", "Skip Opponent's Turn",  "Skip Opponent's Turn"

    };

        drawButton.onClick.AddListener(DrawCard);

        InitializeCardSlots(whiteCards);
        InitializeCardSlots(blackCards);

    }

    public void SoundButton()
    {
        isSoundOn = !isSoundOn;
        sound.text = "Sound: " + (isSoundOn ? "ON" : "OFF");
    }

    public void MusicButton()
    {
        isMusicOn = !isMusicOn;
        music.text = "Music: " + (isMusicOn ? "ON" : "OFF");
    }


    void InitializeCardSlots(CardSlot[] slots)
    {
        foreach (var slot in slots)
        {
            slot.image = slot.cardObject.GetComponent<Image>();
            slot.button = slot.cardObject.GetComponent<Button>();

            // 🔽 Assign the TMP texts by finding child objects
            slot.topText = slot.cardObject.transform.Find("UpperText").GetComponent<TextMeshProUGUI>();
            slot.bottomText = slot.cardObject.transform.Find("BottomText").GetComponent<TextMeshProUGUI>();

            SetCardSlotState(slot, false);

            if (slot.button != null)
            {
                slot.button.onClick.AddListener(() => OnCardClicked(slot));
            }
            else
            {
                Debug.LogWarning("Button not found on cardObject.");
            }
        }
    }


    public void MainScreen()
    {
        SceneManager.LoadScene("StartMenu");
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
        // Check if current player has an extra turn
        if (extraTurnActive > 0)
        {
            extraTurnActive -= 1; // use up the extra turn
            Debug.Log($"{currentPlayer} gets another turn (Skip card used).");

            // Still increase the turn counter and maybe draw a card
            if (currentPlayer == "white")
            {
                whiteTurnCounter++;
                if (whiteTurnCounter >= turnsBeforeDraw)
                {
                    whiteTurnCounter = 0;
                    DrawCardForPlayer("white");
                }
            }
            else
            {
                blackTurnCounter++;
                if (blackTurnCounter >= turnsBeforeDraw)
                {
                    blackTurnCounter = 0;
                    DrawCardForPlayer("black");
                }
            }

            // No player switch
            UpdateCardInteractivity();
            return;
        }

        // Normal player switch
        if (currentPlayer == "white")
        {
            whiteTurnText.gameObject.SetActive(false);
            blackTurnText.gameObject.SetActive(true);

            whiteTurnCounter++;
            if (whiteTurnCounter >= turnsBeforeDraw)
            {
                whiteTurnCounter = 0;
                DrawCardForPlayer("white");
            }

            currentPlayer = "black";
        }
        else
        {
            whiteTurnText.gameObject.SetActive(true);
            blackTurnText.gameObject.SetActive(false);

            blackTurnCounter++;
            if (blackTurnCounter >= turnsBeforeDraw)
            {
                blackTurnCounter = 0;
                DrawCardForPlayer("black");
            }

            currentPlayer = "white";
        }

        UpdateCardInteractivity();
    }


    private void UpdateCardInteractivity()
    {
        foreach (var slot in whiteCards)
        {
            if (slot.button != null)
                slot.button.interactable = currentPlayer == "white" && !string.IsNullOrEmpty(slot.cardName);
        }

        foreach (var slot in blackCards)
        {
            if (slot.button != null)
                slot.button.interactable = currentPlayer == "black" && !string.IsNullOrEmpty(slot.cardName);
        }
    }


    public void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }


    public void Winner(string playerWinner)
    {
        gameOver = true;
        chessBoard.SetActive(false);
        gameOverObject.SetActive(true);
        winnerText.text = playerWinner;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    // ----- CARD DRAWING FUNCTION -----
    public void DrawCard()
    {
        DrawCardForPlayer(currentPlayer);
    }

    private void DrawCardForPlayer(string player)
    {
        if (deck.Count == 0)
        {
            Debug.Log("Deck empty");
            return;
        }

        int randomIndex = Random.Range(0, deck.Count);
        string drawnCard = deck[randomIndex];
        deck.RemoveAt(randomIndex);

        cardDisplay.text = $"{player} drew: {drawnCard}";

        CardSlot[] slots = (player == "white") ? whiteCards : blackCards;

        foreach (var slot in slots)
        {
            if (string.IsNullOrEmpty(slot.cardName))
            {
                slot.cardObject.SetActive(true);
                slot.cardName = drawnCard;

                string cardCategory = GetCardCategory(drawnCard);

                // UI text
                slot.cardObject.transform.Find("UpperText")
                    .GetComponent<TextMeshProUGUI>().text = cardCategory;
                slot.cardObject.transform.Find("BottomText")
                    .GetComponent<TextMeshProUGUI>().text = drawnCard;

                // Sprite
                slot.image.sprite = (cardCategory == "Spawn") ? spawnCardSprite : tacticCardSprite;

                break;
            }
        }

        UpdateCardInteractivity();
    }

    private string GetCardCategory(string cardName)
    {
        switch (cardName)
        {
            case "Add 2 Pawns":
            case "Add Knight":
            case "Add Bishop":
            case "Add Rook":
            case "Add Queen":
                return "Spawn";

            case "Skip Opponent's Turn":
                return "Tactic";

            // Future card categories can go here
            default:
                return "Unknown";
        }
    }


    private string GetCardTopText(string cardName)
    {
        switch (cardName)
        {
            case "Add 2 Pawns": return "Gain Units";
            case "Add Knight": return "Promote to Knight";
            case "Add Bishop": return "Promote to Bishop";
            case "Add Rook": return "Promote to Rook";
            case "Add Queen": return "Promote to Queen";
            default: return "Card Effect";
        }
    }




    void OnCardClicked(CardSlot slot)
    {
        if (string.IsNullOrEmpty(slot.cardName)) return;

        string cardPlayed = slot.cardName;

        ApplyCardEffect(cardPlayed);

        slot.cardName = "";
        slot.cardObject.SetActive(false);

        // Playing a card counts as a turn unless it's a Skip card
        if (cardPlayed != "Skip Opponent's Turn")
        {
            NextTurn();
        }
    }





    void SetCardSlotState(CardSlot slot, bool active)
    {
        if (slot.button != null)
            slot.button.interactable = active;
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
            case "Skip Opponent's Turn":
                SkipOpponentTurn();
                break;
            default:
                Debug.Log("No effect implemented for: " + card);
                break;
        }
    }

    private void SkipOpponentTurn()
    {
        Debug.Log($"{currentPlayer} played 'Skip Opponent's Turn'. They will take another turn.");

        extraTurnActive = 2; // next call to NextTurn() will not switch players
        NextTurn(); // ends current turn and grants another immediately
    }




    private void AddTwoPawns(string player)
    {
        List<Vector2Int> emptySpots = new List<Vector2Int>();

        // Find all empty positions on the board
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (positions[x, y] == null)
                    emptySpots.Add(new Vector2Int(x, y));
            }
        }

        if (emptySpots.Count < 2)
        {
            Debug.Log("Not enough space to spawn 2 pawns.");
            return;
        }

        // Shuffle and choose 2 positions
        for (int i = 0; i < 2; i++)
        {
            Vector2Int spot = emptySpots[Random.Range(0, emptySpots.Count)];
            emptySpots.Remove(spot); // Avoid duplicates

            GameObject pawn = Create(player + "_pawn", spot.x, spot.y);
            SetPosition(pawn);

            if (player == "white")
                playerWhite = AddToArray(playerWhite, pawn);
            else
                playerBlack = AddToArray(playerBlack, pawn);

            Debug.Log($"{player} spawned a pawn at ({spot.x}, {spot.y})");
        }
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
