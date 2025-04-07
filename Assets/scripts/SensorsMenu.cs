using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SensorsMenu : MonoBehaviour
{
    public TextMeshProUGUI accelText;

    private Vector3 lastMousePosition;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        // Simulate accelerometer data based on mouse movement
        Vector3 acceleration = SimulateAccelerometer();

        // Display the simulated accelerometer data
        accelText.text =
            "Accelerometer Data:\n" +
            "X: " + acceleration.x.ToString("F3") + "\n" +
            "Y: " + acceleration.y.ToString("F3");
    }

    // Function to simulate accelerometer data based on mouse movement
    Vector3 SimulateAccelerometer()
    {
        Vector3 simulatedAcceleration = new Vector3();

        // Calculate mouse movement speed (difference between current and last position)
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

        // Update the last mouse position
        lastMousePosition = Input.mousePosition;

        // Use mouse movement speed to simulate accelerometer values
        simulatedAcceleration.x = mouseDelta.x / Screen.width * 2.0f;  // Left-right tilt
        simulatedAcceleration.y = mouseDelta.y / Screen.height * 2.0f;  // Up-down tilt
        simulatedAcceleration.z = 0;  // No Z-axis movement for now

        return simulatedAcceleration;
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
