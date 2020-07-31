using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector3 lastCheckPointPos = new Vector3(0f, 1f, -2f);

    // start: Vector3(0f, 1f, -2f)
    // checkpoint1: Vector3(14.8f, 11.605f, 55.594f)
    // checkpoint2: Vector3(-11.16f, 13.057f, 102.16f)
    // checkpoint3: Vector3(-11.1f, 40.39f, 179.88f)

    public float startTime;
    public float bestTime;
    public float newTime;
    public bool running;
    public Text timeText;

    void Awake()
    {
        // creates a persistent object even when the scene is reloaded
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        } else
        {
            Destroy(gameObject);
        }
        // timer starts running at the beggining and start time is set
        running = true;
        startTime = Time.time;
    }

    void Update()
    {
        // checks if running and updates timer text object
        timeText = GameObject.Find("timeText").GetComponent<Text>();
        if(running)
        {
            float hours = (Time.time - startTime) / 3600;
            float minutes = ((Time.time - startTime) % 3600) / 60;
            float seconds = ((Time.time - startTime) % 3600 ) % 60;
            timeText.text = (int) hours + ":" + (int) minutes + ":" + (int) seconds;
        }
    }

    public void setTime()
    {   
        // gets the time and compares it with the best time 
        newTime = Time.time - startTime;
        bestTime = (float) PlayerPrefs.GetInt("BestTime", 10000);

        if (bestTime > newTime)
        {
            // updates the best time if its better
            PlayerPrefs.SetInt("BestTime", (int) newTime);
        }
    }
}
