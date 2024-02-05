using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastestLap : MonoBehaviour
{
    private Text timeText;
    
    public static FastestLap instance;
    void Awake()
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

    // Start is called before the first frame update
    void Start()
    {
        float fastestLap = EndGame.instance.GetFirstPlace();
        timeText.text = "Fastest Time: " + EndGame.instance.ConvertTimes(fastestLap);
    }

}
