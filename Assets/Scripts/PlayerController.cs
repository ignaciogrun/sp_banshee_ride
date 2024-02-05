using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;


public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    public GameObject gameOverMenu;
    public GameObject SetOfRings1;
    public GameObject SetOfRings2;
    public GameObject SetOfRings3;
    public GameObject SetOfRings4;
    public GameObject SetOfRings5;
    public GameObject PracticeRings;
    public Slider[] sliders;


    //public variables
    public Transform model;

    public Text ringsH;

    private float speed = 230.0f; //speed of the player
    private float modSpeed = 0;

    //private variables
    private float leftVal; //Re-mapping the stearing values
    private float roll; //roll angle

    //private ints
    private int steering; //diference between left and right pull
    public int amountOfRings = 64; //total amount of rings in the level
    public int ringsHit = 0; //amount of rings that the player has gone through

    //Player's rigidBody
    private Rigidbody rb;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        move(); //calls the move player function
        if (Input.GetKeyDown(KeyCode.Escape)) //quit to main menu
        {
            Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameFinished();
        }
        PlatformController.singleton.floatValues[3] = -leftVal * 15; 
        PlatformController.singleton.floatValues[4] = roll * 15; 

        sliders[0].value = (float)BasicSerialThread.instance.left / (Persistance.instance.leftMax - Persistance.instance.leftMin);
        sliders[1].value = (float)BasicSerialThread.instance.right / (Persistance.instance.rightMax - Persistance.instance.rightMin);

        ringsH.text = "Rings Hit: " + ringsHit;
    }



    //Re-maps the range of any 2 values to values from -1 to 1
    public float MapRange(float val, float min, float max, float newMin, float newMax)
    {
        return Mathf.Clamp(((val - min) / (max - min) * (newMax - newMin) + newMin), newMin, newMax);
        // or Y = (X-A)/(B-A) * (D-C) + C
    }



    //Ring trigger - adds score when triggered (not finished)
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Initial Ring")
        {
            ringsHit += 1;
            SetOfRings1.SetActive(true);
            PracticeRings.SetActive(false);
            //start timer
        }
        else if (other.gameObject.tag == "Rings")
        {
            ringsHit += 1;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag == "CheckPoint 1")
        {
            ringsHit += 1;
            SetOfRings1.gameObject.SetActive(false);
            SetOfRings2.gameObject.SetActive(true);
        }
        else if (other.gameObject.tag == "CheckPoint 2")
        {
            ringsHit += 1;
            SetOfRings2.gameObject.SetActive(false);
            SetOfRings3.gameObject.SetActive(true);
        }
        else if (other.gameObject.tag == "CheckPoint 3")
        {
            ringsHit += 1;
            SetOfRings3.gameObject.SetActive(false);
            SetOfRings4.gameObject.SetActive(true);
        }
        else if (other.gameObject.tag == "CheckPoint 4")
        {
            ringsHit += 1;
            SetOfRings4.gameObject.SetActive(false);
            SetOfRings5.gameObject.SetActive(true);
        }
        else if (other.gameObject.tag == "Final Ring")
        {
            AudioManager.instance.EndScene = true;
            print("game ended");
            ringsHit += 1;
            SetOfRings5.SetActive(false);
            
            GameFinished();
        }
    }


    //moves the player every update
    void move()
    {
        steering = BasicSerialThread.instance.right - BasicSerialThread.instance.left;
        modSpeed = ringsHit * 5;
        

        //transform.Translate(0, 0, speed * Time.deltaTime);
        if(rb != null)
            rb.MovePosition(rb.position + transform.forward * (speed + modSpeed)  * Time.deltaTime);

        //yaw
        transform.Rotate(Vector3.up, steering * Time.deltaTime);

        //Roll
        roll = MapRange(steering, -Persistance.instance.leftMax, Persistance.instance.leftMax, -1, 1);
        model.localEulerAngles = new Vector3(60 * -roll, -90, 0);

        //pitch
        leftVal = MapRange(BasicSerialThread.instance.left, Persistance.instance.leftMin, Persistance.instance.leftMax, -1, 1);
        transform.Rotate(transform.right, -leftVal * 90 * Time.deltaTime, Space.World);
        

    } //DO NOT TOUCH!


   
    //Once player triggers final Ring end game
    void GameFinished()
    {
        gameOverMenu.SetActive(true);
        EndGame.instance.SetPlayersTime();
        EndGame.instance.DisplayTimes();
        Time.timeScale = 0;
    }

    public void Quit()
    {
        EditorSceneManager.LoadScene("Main Menu");
    }

}
