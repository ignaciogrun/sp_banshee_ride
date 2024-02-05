using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class EndGame : MonoBehaviour
{
    public static EndGame instance;

    

    private string finalTime; //players time + penalty time

    private int highScorePos; //pos of players high score

    private float[] highScores = new float[5]; //array containing all high scores

    private float penalty; //penalty time to add to the players time
    private float pTime; //players time as a float

    private bool activeHighScore = false;


    //UI texts
    public Text currentScore; //player's current score text
    public Text highScore1; //High score1 text
    public Text highScore2; //High score2 text
    public Text highScore3; //High score3 text
    public Text highScore4; //High score4 text
    public Text highScore5; //High score5 text

    void Awake()
    {
        print("Awake");
        instance = this;
        CheckHighScores();
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    public void SetPlayersTime()
    {
        penalty = (PlayerController.instance.amountOfRings - PlayerController.instance.ringsHit) * 5.0f; //add +5 seconds for every ring missed
        print("penalty: " + penalty);
        print("aR: "+ PlayerController.instance.amountOfRings);
        print("hR: " + PlayerController.instance.ringsHit);

        pTime = TimeManager.instance.GetTimePassed(); //parse players time into a float

        pTime += penalty;  //adds penalty to the players time (final time);

        print("pTime: " + pTime);

        //if (pTime < 0)
        //    pTime *= -1;

        //convert float into string
        finalTime = ConvertTimes(pTime);
        print(finalTime);
        currentScore.text = "Your Time: " + finalTime;

        CompareHighScores();

    } //add penalty to players final time

    void CompareHighScores()
    {
        for(int i = 0; i < highScores.Length; i++)
        {
            if (pTime <= highScores[i])
            {
                activeHighScore = true;
                highScorePos = i;
                break;
            } //if player's time is a high score, save pos in array and exits the loop
        } //compares the playe's score with the high scores

        if (activeHighScore == true)
            SetHighScore();

        activeHighScore = false; //resets the active high score bool
    } //compares the player time with the high score board

    void SetHighScore()
    {
        for(int i = highScorePos; i < highScores.Length; i++) 
        {
            if (i+1  == highScores.Length)
                break;
            highScores[i+1] = highScores[i];
        } //moves all high scores one place to the right

        highScores[highScorePos] = pTime; //set the player's time into the high score board

        SaveScores();
    }

    void SaveScores()
    {
        PlayerPrefs.SetFloat("High Score 1", highScores[0]);
        PlayerPrefs.SetFloat("High Score 2", highScores[1]);
        PlayerPrefs.SetFloat("High Score 3", highScores[2]);
        PlayerPrefs.SetFloat("High Score 4", highScores[3]);
        PlayerPrefs.SetFloat("High Score 5", highScores[4]);

        PlayerPrefs.Save();
    } //saves fastest time leaderboard


    //checks if any of the high scores are null
    //if so, it sets them to 0
    public void CheckHighScores()
    {
        highScores[0] = PlayerPrefs.GetFloat("High Score 1", 600);
        highScores[1] = PlayerPrefs.GetFloat("High Score 2", 600);
        highScores[2] = PlayerPrefs.GetFloat("High Score 3", 600);
        highScores[3] = PlayerPrefs.GetFloat("High Score 4", 600);
        highScores[4] = PlayerPrefs.GetFloat("High Score 5", 600);
    }

    public string ConvertTimes(float time)
    {
        float timeMinutes = Mathf.FloorToInt((time % 3600) / 60); //get the minutes of the player's time
        float timeSeconds = Mathf.FloorToInt(time % 60); // get the amount of seconfs of the player's time
        finalTime = string.Format("{0:00}:{1:00}", timeMinutes, timeSeconds); //parse the player's time into string format
        return finalTime;
    }

    public void DisplayTimes()
    {
        highScore1.text = "1st Place: " + ConvertTimes(highScores[0]);
        highScore2.text = "2nd Place: " + ConvertTimes(highScores[1]);
        highScore3.text = "3rd Place: " + ConvertTimes(highScores[2]);
        highScore4.text = "4th Place: " + ConvertTimes(highScores[3]);
        highScore5.text = "5th Place: " + ConvertTimes(highScores[4]);
    }

    public void Quit()
    {
        EditorSceneManager.LoadScene("Main Menu");
    }

    public float GetFirstPlace()
    {
        return highScores[0];
    }
}
