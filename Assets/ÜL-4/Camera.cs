using UnityEngine;
using UnityEngine.UI;

public class CameraPreview : MonoBehaviour
{
    public RawImage rawImage;
    private WebCamTexture webcamTexture;
    public GameObject mainPanel;
    public GameObject cameraPanel;

    public void StartCamera()
    {
        mainPanel.SetActive(false);
        cameraPanel.SetActive(true);

        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length > 0)
        {
            // Pick the back camera (optional)
            int backCamIndex = 0;
            for (int i = 0; i < devices.Length; i++)
            {
                if (!devices[i].isFrontFacing)
                {
                    backCamIndex = i;
                    break;
                }
            }

            webcamTexture = new WebCamTexture(devices[backCamIndex].name);
            rawImage.texture = webcamTexture;
            rawImage.material.mainTexture = webcamTexture;
            webcamTexture.Play();
        }
        else
        {
            Debug.Log("No camera found");
        }
    }
    public void Back()
    {
        mainPanel.SetActive(true);
        cameraPanel.SetActive(false);
        webcamTexture.Stop();
    }
}
