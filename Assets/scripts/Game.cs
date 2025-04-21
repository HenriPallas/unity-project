using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;




public class Game : MonoBehaviour
{

    private float shakeThreshold = 2.0f;
    private float accelerometerUpdateInterval = 0.1f;
    private float lowPassKernelWidthInSeconds = 1.0f;
    private float lowPassFilterFactor;
    private Vector3 lowPassValue;

    public GameObject[,] chessPieces = new GameObject[8, 8];

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
        drawButton.onClick.AddListener(DrawCard);

        //acceleromeetri start
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Input.acceleration;



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
        currentPlayer = (currentPlayer == "white") ? "black" : "white";
    }

    public void Update()
    {

        if (gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }


        #if UNITY_EDITOR || UNITY_STANDALONE
    if (Input.GetKeyDown(KeyCode.S))
    {
        Debug.Log("Simulated Shake!");
        RandomizePiecePositions();
    }

    #else
    // Real accelerometer logic
    Vector3 acceleration = Input.acceleration;
    lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
    Vector3 deltaAcceleration = acceleration - lowPassValue;

    if (deltaAcceleration.sqrMagnitude >= shakeThreshold * shakeThreshold)
    {
        RandomizePiecePositions();
    }
#endif



    }

    // ----Shaking piece randomizer---
    public void RandomizePiecePositions()
{
    // Clear the board
    for (int x = 0; x < 8; x++)
        for (int y = 0; y < 8; y++)
            SetPositionEmpty(x, y);

    List<Vector2Int> availablePositions = new List<Vector2Int>();
    for (int x = 0; x < 8; x++)
        for (int y = 0; y < 8; y++)
            availablePositions.Add(new Vector2Int(x, y));

    List<GameObject> allPieces = new List<GameObject>(playerWhite);
    allPieces.AddRange(playerBlack);

    foreach (GameObject piece in allPieces)
    {
        if (availablePositions.Count == 0) break;

        int randIndex = Random.Range(0, availablePositions.Count);
        Vector2Int pos = availablePositions[randIndex];
        availablePositions.RemoveAt(randIndex);

        Chessman cm = piece.GetComponent<Chessman>();
        cm.SetXBoard(pos.x);
        cm.SetYBoard(pos.y);
        SetPosition(piece);
        cm.SetCoords(); // Move the GameObject visually
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
        if (deck.Count > 0)
        {
            int randomIndex = Random.Range(0, deck.Count);
            string drawnCard = deck[randomIndex];
            deck.RemoveAt(randomIndex);

            // Update the UI to show the drawn card
            cardDisplay.text = "You drew: " + drawnCard;
        }
        else
        {
            cardDisplay.text = "Deck is empty!";
        }
    }

    


    public List<GameObject> GetAllChessPieces()
{
    List<GameObject> allPieces = new List<GameObject>();

    foreach (GameObject piece in chessPieces)
    {
        if (piece != null)
        {
            allPieces.Add(piece);
        }
    }

    return allPieces;
}


public void ClearBoard()
{
    GameObject[] pieces = GameObject.FindGameObjectsWithTag("ChessPiece");

    foreach (GameObject piece in pieces)
    {
        Destroy(piece);
    }

    chessPieces = new GameObject[8, 8];
    positions = new GameObject[8, 8]; // if you're using another tracking array
}




public void SpawnPiece(string name, int x, int y)
{
    Debug.Log($"[SpawnPiece] {name} at board coords ({x},{y})");
    // load & instantiate your generic chesspiece prefab…
    GameObject prefab = Resources.Load<GameObject>("prefab/chesspiece");
    GameObject obj = Instantiate(prefab);

    // name it so Activate() picks the right sprite
    obj.tag = "ChessPiece";
    obj.name = name;

    // set its board coords
    Chessman cm = obj.GetComponent<Chessman>();
    cm.SetXBoard(x);
    cm.SetYBoard(y);

    // *this is the missing line* — register it in your positions matrix
    SetPosition(obj);  // <-- positions[x,y] = obj;

    // also keep your chessPieces array if you use it
    chessPieces[x, y] = obj;

    // finally, update the world and sprite
    cm.Activate();
}





public void SaveGame()
{
    Debug.Log("Saving...");
    Debug.Log("Persistent Path: " + Application.persistentDataPath);
    Chessman[,] chessPieces = new Chessman[8, 8];

    // Convert GameObject[,] to Chessman[,]
    for (int x = 0; x < 8; x++)
    {
        for (int y = 0; y < 8; y++)
        {
            if (positions[x, y] != null)
            {
                chessPieces[x, y] = positions[x, y].GetComponent<Chessman>();
            }
        }
    }

    // Save currentPlayer if needed, or any other game state
    string currentPlayer = GetCurrentPlayer(); // or however you store the turn

    SaveData data = new SaveData(chessPieces, currentPlayer);
    string json = JsonUtility.ToJson(data);

    string path = Application.persistentDataPath + "/savefile.json";
    System.IO.File.WriteAllText(path, json);

    Debug.Log("Game saved to: " + path);
}



}
