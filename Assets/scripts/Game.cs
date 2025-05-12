using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;





public class Game : MonoBehaviour
{


    private int turnCounter = 0; // Counts the number of total turns
    List<string> whitePlayerCards = new List<string>();
    List<string> blackPlayerCards = new List<string>();
    
    public TextMeshProUGUI whiteCardDisplayText;
    public TextMeshProUGUI blackCardDisplayText;
    
    
    // Reference from Unity IDE
    public GameObject chesspiece;

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
        deck = new List<string> {  "Add 2 Pawns", "Queens Move Diagonally Only", "Swap a Knight and a Bishop", "Pawns Move Backward", "Instant Promotion", "Rook Teleport", "King’s Shield", "Bishop Frenzy", "Steal a Move", "Reverse Attack" };
        //drawButton.onClick.AddListener(DrawCard);



    }



    public void ReturnToMenu()
{
    SceneManager.LoadScene("StartMenu"); 
}





    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
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
    turnCounter++;

    // Every 3 turns, draw a card for the current player
    if (turnCounter % 3 == 0)
    {
        string newCard = DrawCard();

        if (currentPlayer == "white")
        {
            whitePlayerCards.Add(newCard);
            DisplayPlayerCard("white", newCard);
        }
        else
        {
            blackPlayerCards.Add(newCard);
            DisplayPlayerCard("black", newCard);
        }
    }

    // Alternate turn
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
    public string DrawCard()
{
    if (deck.Count > 0)
    {
        int randomIndex = Random.Range(0, deck.Count);
        string drawnCard = deck[randomIndex];
        deck.RemoveAt(randomIndex);

        // Update the UI
        cardDisplay.text = "You drew: " + drawnCard;

        return drawnCard;  // return the drawn card
    }

    // If deck is empty
    cardDisplay.text = "Deck is empty!";
    return "No card drawn";  //return a fallback string (not null)
}


    public void DisplayPlayerCard(string player, string cardText)
{
    if (player == "white" && whiteCardDisplayText != null)
    {
        whiteCardDisplayText.text = "White drew: " + cardText;
    }
    else if (player == "black" && blackCardDisplayText != null)
    {
        blackCardDisplayText.text = "Black drew: " + cardText;
    }
}




}
