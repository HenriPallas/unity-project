using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Image : MonoBehaviour
{
    public RawImage imageDisplay;
    private string imageUrl = "https://reqres.in/img/faces/2-image.jpg"; // Näidispilt

    public void PressLoadImage()
    {
        StartCoroutine(LoadImage());
    }

    IEnumerator LoadImage()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            imageDisplay.texture = tex;
        }
        else
        {
            Debug.LogError("Pildi laadimise viga: " + request.error);
        }
    }
}
