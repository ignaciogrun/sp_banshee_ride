using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    PlayerController player; 
    
    public GameObject CalibrationMenu;
    public GameObject HighScoresMenu;
    public GameObject CreditsMenu;
    public GameObject ExitGame;

    public Toggle[] toggles;

    bool CalibrationDone;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Debug.Log(Persistance.instance);

        CalibrationDone = false;
        BasicSerialThread.instance.Init("COM4", 9600);
        PlatformController.singleton.Init("COM3", 115200);

    }

    public void CloseMenus()
    {
        CalibrationMenu.SetActive(false);
        HighScoresMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        ExitGame.SetActive(false);
    }

    public void Play()
    {
        CloseMenus();
        Persistance.instance.leftMin = PlayerPrefs.GetInt("leftMin");
        Persistance.instance.rightMin = PlayerPrefs.GetInt("rightMin");
        Persistance.instance.leftMax = PlayerPrefs.GetInt("leftMax");
        Persistance.instance.rightMax = PlayerPrefs.GetInt("rightMax");
        EditorSceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }

    public void Calibration()
    {
        CloseMenus();
        CalibrationMenu.SetActive(true);
        CalibrationDone = false;
    }

    public void FastestTimes()
    {
        CloseMenus();

        HighScoresMenu.SetActive(true);
        EndGame.instance.CheckHighScores();
        EndGame.instance.DisplayTimes();
    }

    public void Credits()
    {
        CloseMenus();
        CreditsMenu.SetActive(true);
    }
    private void Update()
    {
        if (CalibrationMenu.activeSelf)
        {
            //Calibration
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                Persistance.instance.leftMin = BasicSerialThread.instance.left;
                toggles[0].isOn = true;
                print("left min" + Persistance.instance.leftMin);
            }   //sets the min value for left rope
            else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                Persistance.instance.leftMax = BasicSerialThread.instance.left;
                toggles[1].isOn = true;
                print("left max" + Persistance.instance.leftMax);
            }   //sets the max value for left rope
            else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                Persistance.instance.rightMin = BasicSerialThread.instance.right;
                toggles[2].isOn = true;
                print("right min" + Persistance.instance.rightMin);
            }   //sets the min value for right rope
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                Persistance.instance.rightMax = BasicSerialThread.instance.right;
                toggles[3].isOn = true;
                print("right max" + Persistance.instance.rightMax);
            }   //sets the max value for right rope

            //setting up left & right midpoints
            Persistance.instance.halfLeft = (Persistance.instance.leftMax - Persistance.instance.leftMin) / 2; //setting left midpoint
            Persistance.instance.halfRight = (Persistance.instance.rightMax - Persistance.instance.rightMin) / 2; //setting right midpoint

            //check if calibration has been completed 
            if(toggles[0].isOn == true && toggles[2].isOn == true && toggles[2].isOn == true && toggles[3].isOn == true)
            {
                CalibrationDone = true;
            }


            //Saves Calibrarion settings to PlayerPrefs.
            PlayerPrefs.SetInt("leftMin", Persistance.instance.leftMin);
            PlayerPrefs.SetInt("rightMin", Persistance.instance.rightMin);
            PlayerPrefs.SetInt("leftMax", Persistance.instance.leftMax);
            PlayerPrefs.SetInt("rightMax", Persistance.instance.rightMax);

            PlayerPrefs.Save();
        } //calibration
    }

    public void ConfirmExit()
    {
        Application.Quit(1);
    }
}
