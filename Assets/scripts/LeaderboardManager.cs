using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class JsonPlaceholderUser
{
    public int id;
    public string name;
    public string username;
    public string email;
}

[System.Serializable]
public class JsonPlaceholderResponse
{
    public JsonPlaceholderUser[] users;
}

public class LeaderboardManager : MonoBehaviour
{
    public string leaderboardURL = "https://jsonplaceholder.typicode.com/users";
    public TMP_Text leaderboardText;

    public void LoadLeaderboard()
    {
        StartCoroutine(GetLeaderboard());
    }

    IEnumerator GetLeaderboard()
    {
        Debug.Log("Requesting URL: " + leaderboardURL);
        UnityWebRequest request = UnityWebRequest.Get(leaderboardURL);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received leaderboard data: " + request.downloadHandler.text);

            // Deserialize the JSON response
            JsonPlaceholderResponse data = JsonUtility.FromJson<JsonPlaceholderResponse>("{\"users\":" + request.downloadHandler.text + "}");
            DisplayLeaderboard(data);
        }
        else
        {
            Debug.LogError("Failed to load leaderboard: " + request.error);
        }
    }

    void DisplayLeaderboard(JsonPlaceholderResponse data)
    {
        leaderboardText.text = "";

        foreach (JsonPlaceholderUser user in data.users)
        {
            string fullName = user.name;  // Just using the full name directly
            int fakeScore = user.id * 100;  // Fake score calculation

            leaderboardText.text += fullName + " - " + fakeScore + "\n";
        }
    }
}
