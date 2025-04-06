using UnityEngine;

public class Mikrofon : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject microphonePanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OpenMicrophone() {
        mainPanel.SetActive(false);
        microphonePanel.SetActive(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
