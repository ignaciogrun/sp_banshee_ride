using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    
    public static TimeManager instance;
    
    private Text timeText;
    
    private float timePassed;
    private float minutesPassed;
    private float secondsPassed;
    private string time;
    

    private bool textEnabled;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        timeText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        
        timePassed += Time.deltaTime;

        minutesPassed = Mathf.FloorToInt((timePassed % 3600) / 60);
        secondsPassed = Mathf.FloorToInt(timePassed % 60);

        time = string.Format("{0:00}:{1:00}", minutesPassed, secondsPassed);
        timeText.text = "Your Time: " + time; 
    }

    public float GetTimePassed()
    {
        return timePassed;
    }

    public void SetTimePassed(float setValue)
    {
        this.timePassed = setValue;
    }

    public void EnableText()
    {
        minutesPassed = Mathf.FloorToInt((timePassed % 3600) / 60);
        secondsPassed = Mathf.FloorToInt(timePassed % 60);

        time = string.Format("{0:00}:{1:00}", minutesPassed, secondsPassed);

        textEnabled = true;
        timeText.gameObject.SetActive(true);
        timeText.text = "Time: " + time;
        PlayerPrefs.SetInt("Timer Int", 1);
    }

    public void DisableText()
    {
        textEnabled = false;
        timeText.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Timer Int", 0);
    }

    public string GetTime()
    {
        return time;
    }
}
