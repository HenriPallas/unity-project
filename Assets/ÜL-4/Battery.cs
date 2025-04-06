using UnityEngine;
using UnityEngine.UI;


public class Battery : MonoBehaviour
{
    public Text batteryText;
    public GameObject mainPanel;
    public GameObject batteryPanel;

    public void openBatteryPanel()
    {
        mainPanel.SetActive(false);
        batteryPanel.SetActive(true);
    }

    void Update()
    {
        float batteryLevel = SystemInfo.batteryLevel;
        BatteryStatus status = SystemInfo.batteryStatus;

        string statusString = "";

        if (status == BatteryStatus.Charging)
        {
            statusString = "Charging";
        }
        else if (status == BatteryStatus.Discharging)
        {
            statusString = "Discharging";
        }
        else if (status == BatteryStatus.Full)
        {
            statusString = "Full";
        }
        else if (status == BatteryStatus.NotCharging)
        {
            statusString = "Not Charging";
        }
        else
        {
            statusString = "Unknown";
        }



        if (batteryLevel >= 0)
        {
            batteryText.text = $"Battery: {(batteryLevel * 100f):0}% ({statusString})";
        }
        else
        {
            batteryText.text = "Battery info not available";
        }
    }
    public void Back()
    {
        mainPanel.SetActive(true);
        batteryPanel.SetActive(false);
    }
}
