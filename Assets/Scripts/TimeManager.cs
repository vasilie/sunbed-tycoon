using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance;

    public static TimeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TimeManager>();
                if (instance == null)
                {
                    // Create a new TimeManager GameObject if one doesn't exist
                    GameObject timeManagerObject = new GameObject("TimeManager");
                    instance = timeManagerObject.AddComponent<TimeManager>();
                }
            }
            return instance;
        }
    }

    public float currentTimeOfDay = 12f * 60f;
    public float timeScale = 10.0f;
    public float minute = 0;
    public float hour = 0;
    public string timeInString = "";
    private string hourString = "";
    private string minuteString = "";
    void Awake()
    {
        // Ensure there's only one instance
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
    {
        // Update the current time of day based on real-time or game time
        currentTimeOfDay += Time.deltaTime * timeScale;
        currentTimeOfDay %= 1440f; // Keep it between 0 and 1440, since there are 1440 minutes in a day
        hour = (int)currentTimeOfDay / 60;

        minute = (int)currentTimeOfDay % 60;

        if (hour < 10)
        {
            hourString = "0" + hour.ToString();
        }
        else
        {
            hourString = hour.ToString();
        }
        if (minute < 10)
        {
            minuteString = "0" + minute.ToString();
        }
        else
        {
            minuteString = minute.ToString();
        }

        timeInString = hourString + ":" + minuteString;
    }
}