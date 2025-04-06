using UnityEngine;
using UnityEngine.UI;

public class Mikrofon : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject microphonePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OpenMicrophone() {
        mainPanel.SetActive(false);
        microphonePanel.SetActive(true);
    }

    public AudioSource audioSource;
    private bool isRecording = false;
    private string selectedDevice;

    [Header("UI Settings")]
    public Button toggleButton;
    public Text statusText;

    void Start()
    {
        
        // Kontrolli mikrofone
        if (Microphone.devices.Length == 0)
        {
            statusText.text = "Mikrofoni ei leitud!";
            toggleButton.interactable = false;
            return;
        }

        selectedDevice = Microphone.devices[0];
        statusText.text = "Mikrofon valmis";
        toggleButton.onClick.AddListener(ToggleMicrophone);
    }

    void ToggleMicrophone()
    {
        if (isRecording)
        {
            // Lõpeta salvestamine
            Microphone.End(selectedDevice);
            audioSource.Stop();
            statusText.text = "Mikrofon välja lülitatud";
        }
        else
        {
            // Alusta salvestamist
            audioSource.clip = Microphone.Start(selectedDevice, true, 10, 44100);
            
            // Oota kuni mikrofon käivitub
            while (!(Microphone.GetPosition(selectedDevice) > 0)) { }
            
            audioSource.loop = true;
            audioSource.Play();
            statusText.text = "Mikrofon sisse lülitatud";
        }

        isRecording = !isRecording;
    }

    void OnDestroy()
    {
        if (isRecording)
        {
            Microphone.End(selectedDevice);
        }
    }

    public void back()
    {
        mainPanel.SetActive(true);
        microphonePanel.SetActive(false);
    }
}


