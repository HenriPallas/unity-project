using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System;

[Serializable]
public class UserData
{
    public List<User> data;
}

[Serializable]
public class User
{
    public int id;
    public string email;
    public string first_name;
    public string last_name;
    public string avatar;
}

[Serializable]
public class CreatedUser
{
    public string name;
    public string job;
    public string id;
    public string createdAt;
}

public class ServerManager : MonoBehaviour
{
    public Text outputText; // viide UI Text elemendile
    private string serverResponse; // serveri toores vastus

    public void OnGetUsersButtonPressed()
    {
        StartCoroutine(GetUsers());
    }

    public void OnSendUserDataButtonPressed()
    {
        StartCoroutine(SendUserData());
    }

    IEnumerator GetUsers()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://reqres.in/api/users?page=2");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            serverResponse = request.downloadHandler.text;
            DisplayUsers();
        }
        else
        {
            Debug.LogError("Viga: " + request.error);
        }
    }

    IEnumerator SendUserData()
    {
        string json = "{\"name\":\"TestUser\", \"job\":\"Developer\"}";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest request = new UnityWebRequest("https://reqres.in/api/users", UnityWebRequest.kHttpVerbPOST);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Lisa see uus kohustuslik API võti siia:
        request.SetRequestHeader("x-api-key", "reqres-free-v1");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            serverResponse = request.downloadHandler.text;
            DisplayCreatedUser();
        }
        else
        {
            Debug.LogError("Viga saatmisel: " + request.responseCode + " " + request.error);
            Debug.LogError("Serveri vastus: " + request.downloadHandler.text);
        }
    }



    void DisplayUsers()
    {
        UserData users = JsonUtility.FromJson<UserData>(serverResponse);
        string displayText = "Kasutajad:\n";

        foreach (var user in users.data)
        {
            Debug.Log("Json: " + serverResponse);
            displayText += $"{user.first_name} {user.last_name}\n";
        }

        if (outputText != null)
        {
            outputText.text = displayText;
        }
    }

    void DisplayCreatedUser()
    {
        CreatedUser newUser = JsonUtility.FromJson<CreatedUser>(serverResponse);
        string displayText = $"Uus kasutaja loodud:\nNimi: {newUser.name}\nTöö: {newUser.job}";

        if (outputText != null)
        {
            outputText.text = displayText;
        }
    }
}
