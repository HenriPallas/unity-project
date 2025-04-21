using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGameManager : MonoBehaviour
{
    public Game game; 

    public void SaveGame()
    {
        List<ChessPieceData> saveData = new List<ChessPieceData>();

        foreach (GameObject piece in game.GetAllChessPieces())
        {
            Chessman cm = piece.GetComponent<Chessman>();
            ChessPieceData data = new ChessPieceData()
            {
                name = cm.name,
                x = cm.GetXBoard(),
                y = cm.GetYBoard()
            };

            saveData.Add(data);
        }

        string json = JsonUtility.ToJson(new ChessPieceDataList { pieces = saveData });
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

        
    public void LoadGame()
{
    string path = Application.persistentDataPath + "/savefile.json";
    
    // Ensure the file exists before attempting to load
    if (File.Exists(path))
    {
        // Read JSON content from file
        string json = File.ReadAllText(path);
        
        // Deserialize JSON into your chess piece data
        ChessPieceDataList dataList = JsonUtility.FromJson<ChessPieceDataList>(json);

        // Clear the current board of pieces
        game.ClearBoard();

        // Iterate through each piece data and spawn pieces
        foreach (ChessPieceData data in dataList.pieces)
        {
            // Check if the prefab name exists and spawn the piece at the right position
            game.SpawnPiece(data.name, data.x, data.y);
        }
    }
    else
    {
        Debug.LogWarning("No save file found.");
    }
}

    


    [System.Serializable]
    public class ChessPieceDataList
    {
        public List<ChessPieceData> pieces;
    }
}